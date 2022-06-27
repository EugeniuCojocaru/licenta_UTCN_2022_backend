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
    [Route("api/institutions")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionRepository institutionRepository, IFacultyRepository facultyRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionWithoutFacultyDto>>> GetInstitutions()
        {
            var institutionEntities = await _institutionRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<InstitutionWithoutFacultyDto>>(institutionEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetInstitution(Guid id, bool includeFaculties = false)
        {
            var institution = await _institutionRepository.GetById(id);
            if (institution == null)
            {
                return NotFound();
            }
            if (includeFaculties)
            {
                return Ok(_mapper.Map<InstitutionDto>(institution));
            }
            return Ok(_mapper.Map<InstitutionWithoutFacultyDto>(institution));

        }

        [HttpGet("{id}/faculties")]
        public async Task<ActionResult> GetFacultyByInstitutionId(Guid id)
        {
            var institution = await _institutionRepository.GetById(id);

            if (institution == null)
            {
                return NotFound();
            }
            var facultyEntities = await _facultyRepository.GetAllByInstitutionId(id);

            return Ok(_mapper.Map<IEnumerable<FacultyWithoutDepartmentDto>>(facultyEntities));

        }

        [HttpPost]
        public async Task<ActionResult<InstitutionDto>> CreateInstitution(InstitutionCreateDto newInstitution)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            if (await _institutionRepository.Exists(newInstitution.Name))
            {
                return Conflict("Same name not allowed");
            }
            var dbInstitution = _mapper.Map<Entities.Institution>(newInstitution);

            await _institutionRepository.CreateInstitution(dbInstitution);


            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Institution,
            };
            await _auditRepository.CreateAudit(dbAudit);

            var institutionToReturn = _mapper.Map<InstitutionDto>(dbInstitution);
            return Ok(institutionToReturn);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateInstitution(InstitutionUpdateDto updatedInstitution)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            var oldInstitution = await _institutionRepository.GetById(updatedInstitution.Id);
            if (oldInstitution == null)
            {
                return NotFound();
            }

            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Institution,
                Notes = oldInstitution.Name + " -> " + updatedInstitution.Name,
            };
            _mapper.Map(updatedInstitution, oldInstitution);
            await _institutionRepository.SaveChanges();


            await _auditRepository.CreateAudit(dbAudit);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteInstitution(Guid institutionId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var institution = await _institutionRepository.GetById(institutionId);
            if (institution == null)
            {
                return NotFound("Institution does not exist");
            }

            _institutionRepository.DeleteInstitution(institution);
            await _institutionRepository.SaveChanges();
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Institution,
                Notes = institution.Name
            };
            await _auditRepository.CreateAudit(dbAudit);
            return Ok();

        }
    }
}
