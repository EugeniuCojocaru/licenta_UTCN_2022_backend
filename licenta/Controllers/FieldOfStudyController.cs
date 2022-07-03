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
    [Route("api/fieldsOfStudy")]
    public class FieldOfStudyController : Controller
    {
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public FieldOfStudyController(IFieldOfStudyRepository fieldOfStudyRepository, IDepartmentRepository departmentRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldOfStudyDto>>> GetFieldsOfStudy()
        {
            var fieldsOfStudyEntities = await _fieldOfStudyRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FieldOfStudyDto>>(fieldsOfStudyEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FieldOfStudyDto>> GetFieldOfStudy(Guid id)
        {
            var fieldOfStudy = await _fieldOfStudyRepository.GetById(id);
            if (fieldOfStudy == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FieldOfStudyDto>(fieldOfStudy));

        }

        [HttpPost]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<FieldOfStudyDto>> CreateFieldOfStudy(FieldOfStudyCreateDto fieldOfStudyDto)
        {
            if (!await _departmentRepository.Exists(fieldOfStudyDto.DepartmentId))
            {
                return NotFound();
            }

            var fieldOfStudy = _mapper.Map<Entities.FieldOfStudy>(fieldOfStudyDto);

            await _departmentRepository.AddFieldOfStudyToDepartment(fieldOfStudyDto.DepartmentId, fieldOfStudy);

            await _departmentRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.FieldOfStudy,
            };
            await _auditRepository.CreateAudit(dbAudit);

            var fieldOfStudyToReturn = _mapper.Map<FieldOfStudyDto>(fieldOfStudy);

            return Ok(fieldOfStudyToReturn);
        }

        [HttpPut]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> UpdateFieldOfStudy(Guid departmentId, InstitutionUpdateDto fieldOfStudyDto)
        {
            if (!await _departmentRepository.Exists(departmentId))
            {
                return NotFound("Department not found");
            }

            var oldFieldOfStudy = await _fieldOfStudyRepository.GetById(fieldOfStudyDto.Id);
            if (oldFieldOfStudy == null || oldFieldOfStudy.DepartmentId != departmentId) { return NotFound("FieldOfStudy not found for the department"); }

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.FieldOfStudy,
                Notes = oldFieldOfStudy.Name + " -> " + fieldOfStudyDto.Name,
            };

            _mapper.Map(fieldOfStudyDto, oldFieldOfStudy);
            await _fieldOfStudyRepository.SaveChanges();
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> DeleteFieldOfStudy(Guid departmentId, Guid fieldOfStudyId)
        {
            if (!await _departmentRepository.Exists(departmentId))
            {
                return NotFound("Department not found");
            }

            var oldFieldOfStudy = await _fieldOfStudyRepository.GetById(fieldOfStudyId);
            if (oldFieldOfStudy == null || oldFieldOfStudy.DepartmentId != departmentId) { return NotFound("FieldOfStudy not found for the department"); }

            _fieldOfStudyRepository.DeleteFieldOfStudy(oldFieldOfStudy);
            await _fieldOfStudyRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.FieldOfStudy,
                Notes = oldFieldOfStudy.Name
            };
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }
    }
}
