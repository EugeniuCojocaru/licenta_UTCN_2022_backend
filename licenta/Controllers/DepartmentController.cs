using AutoMapper;
using licenta.Entities;
using licenta.Models.InstitutionHierarchy;
using licenta.Services.Audits;
using licenta.Services.InstitutionHierarchy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository, IFieldOfStudyRepository fieldOfStudyRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentWithoutFieldOfStudyDto>>> GetDepartments()
        {
            var departmentEntities = await _departmentRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<DepartmentWithoutFieldOfStudyDto>>(departmentEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(Guid departmentId, bool includeFiledsOfStudy = false)
        {
            var department = await _departmentRepository.GetById(departmentId);
            if (department == null)
            {
                return NotFound();
            }
            if (includeFiledsOfStudy)
            {
                return Ok(_mapper.Map<DepartmentDto>(department));
            }
            return Ok(_mapper.Map<DepartmentWithoutFieldOfStudyDto>(department));

        }

        [HttpGet("{departmentId}/fieldsOfStudy")]
        public async Task<ActionResult> GetFieldsOfStudyByDepartmentId(Guid departmentId)
        {
            var department = await _departmentRepository.GetById(departmentId);

            if (department == null)
            {
                return NotFound();
            }
            var fieldsOfStudyEntities = await _fieldOfStudyRepository.GetAllByDepartmentId(departmentId);

            return Ok(_mapper.Map<IEnumerable<FieldOfStudyDto>>(fieldsOfStudyEntities));

        }

        [HttpPost]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<DepartmentWithoutFieldOfStudyDto>> CreateDepartment(DepartmentCreateDto departmentDto)
        {
            if (!await _facultyRepository.Exists(departmentDto.FacultyId))
            {
                return NotFound();
            }

            var department = _mapper.Map<Entities.Department>(departmentDto);

            await _facultyRepository.AddDepartmentToFaculty(departmentDto.FacultyId, department);

            await _facultyRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Department,
            };
            await _auditRepository.CreateAudit(dbAudit);

            var departmentToReturn = _mapper.Map<DepartmentWithoutFieldOfStudyDto>(department);

            return Ok(departmentToReturn);
        }

        [HttpPut]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> UpdateDepartment(Guid facultyId, InstitutionUpdateDto departmentDto)
        {
            if (!await _facultyRepository.Exists(facultyId))
            {
                return NotFound("Faculty not found");
            }

            var oldDepartment = await _departmentRepository.GetById(departmentDto.Id);
            if (oldDepartment == null || oldDepartment.FacultyId != facultyId) { return NotFound("Department not found for the faculty"); }

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Department,
                Notes = oldDepartment.Name + " -> " + departmentDto.Name,
            };

            _mapper.Map(departmentDto, oldDepartment);
            await _departmentRepository.SaveChanges();
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> DeleteDepartment(Guid facultyId, Guid departmentId)
        {
            if (!await _facultyRepository.Exists(facultyId))
            {
                return NotFound("Faculty not found");
            }

            var oldDepartment = await _departmentRepository.GetById(departmentId);
            if (oldDepartment == null || oldDepartment.FacultyId != facultyId) { return NotFound("Department not found for the faculty"); }

            _departmentRepository.DeleteDepartment(oldDepartment);
            await _departmentRepository.SaveChanges();
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Department,
                Notes = oldDepartment.Name
            };
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }
    }
}
