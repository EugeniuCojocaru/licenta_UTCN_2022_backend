using AutoMapper;
using licenta.Entities;
using licenta.Models.Audits;
using licenta.Services.Audits;
using licenta.Services.Teachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [Route("api/audit")]
    [Authorize]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public AuditController(IAuditRepository auditRepository, ITeacherRepository teacherRepository, IMapper mapper)
        {
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditDto>>> GetAudits()
        {
            var auditEntities = await _auditRepository.GetAll();
            var audits = new List<AuditDto>();
            foreach (Audit a in auditEntities)
            {
                var userName = await _teacherRepository.GetById(a.UserId);
                var auditDto = new AuditDto
                {
                    User = userName.Name,
                    CreatedAt = a.CreatedAt,
                    Entity = a.Entity.ToString(),
                    Operation = a.Operation.ToString(),
                    Notes = a.Notes,
                };
                audits.Add(auditDto);
            }
            return Ok(audits);

        }
    }
}
