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
    [Route("api/faculties")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyRepository facultyRepository, IInstitutionRepository institutionRepository, IDepartmentRepository departmentRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyWithoutDepartmentDto>>> GetFaculties()
        {
            var facultyEntities = await _facultyRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FacultyWithoutDepartmentDto>>(facultyEntities));

        }

        [HttpGet("{facultyId}", Name = "GetFaculty")]
        public async Task<ActionResult> GetFaculty(Guid facultyId, bool includeDepartments = false)
        {
            var faculty = await _facultyRepository.GetById(facultyId);
            if (faculty == null)
            {
                return NotFound();
            }
            if (includeDepartments)
            {
                return Ok(_mapper.Map<FacultyDto>(faculty));
            }
            return Ok(_mapper.Map<FacultyWithoutDepartmentDto>(faculty));

        }

        [HttpGet("{facultyId}/departments")]
        public async Task<ActionResult> GetDepartmentsByFacultyId(Guid facultyId)
        {
            var faculty = await _facultyRepository.GetById(facultyId);

            if (faculty == null)
            {
                return NotFound();
            }
            var departmentEntities = await _departmentRepository.GetAllByFacultyId(facultyId);

            return Ok(_mapper.Map<IEnumerable<DepartmentWithoutFieldOfStudyDto>>(departmentEntities));

        }

        [HttpPost]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<FacultyWithoutDepartmentDto>> CreateFaculty(FacultyCreateDto facultyDto)
        {
            if (!await _institutionRepository.Exists(facultyDto.InstitutionId))
            {
                return NotFound();
            }

            var faculty = _mapper.Map<Entities.Faculty>(facultyDto);

            await _institutionRepository.AddFacultyToInstitution(facultyDto.InstitutionId, faculty);

            await _institutionRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Faculty,
            };
            await _auditRepository.CreateAudit(dbAudit);

            var facultyToReturn = _mapper.Map<FacultyWithoutDepartmentDto>(faculty);

            return Ok(facultyToReturn);
        }

        [HttpPut]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> UpdateFaculty(Guid institutionId, InstitutionUpdateDto facultyDto)
        {
            if (!await _institutionRepository.Exists(institutionId))
            {
                return NotFound("Institution not found");
            }

            var oldFaculty = await _facultyRepository.GetById(facultyDto.Id);
            if (oldFaculty == null || oldFaculty.InstitutionId != institutionId) { return NotFound("Faculty not found for the institution"); }
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Faculty,
                Notes = oldFaculty.Name + " -> " + facultyDto.Name,
            };

            _mapper.Map(facultyDto, oldFaculty);
            await _facultyRepository.SaveChanges();
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }


        [HttpDelete]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> DeleteFaculty(Guid facultyId, Guid institutionId)
        {
            if (!await _institutionRepository.Exists(institutionId))
            {
                return NotFound("Institution not found");
            }

            var faculty = await _facultyRepository.GetById(facultyId);
            if (faculty == null || faculty.InstitutionId != institutionId)
            { return NotFound("Faculty not found for the institution"); }


            _facultyRepository.DeleteFaculty(faculty);
            await _institutionRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Faculty,
                Notes = faculty.Name
            };
            await _auditRepository.CreateAudit(dbAudit);
            return Ok();

        }
    }
}
