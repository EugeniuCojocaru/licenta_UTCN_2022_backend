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
        [Authorize(Policy = "MustBeAdmin")]
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

        [HttpGet("/activity/month")]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<IEnumerable<AuditDto>>> GetStatistics()
        {
            var auditEntities = await _auditRepository.GetByMonth();
            var statistics = new List<StatsDto>();
            foreach (Audit a in auditEntities)
            {
                var v = statistics.FindIndex(s => s.Label.Equals(a.CreatedAt.Day.ToString()));
                if (v != -1)
                {
                    statistics.ElementAt(v).Value++;
                }
                else
                {
                    statistics.Add(new StatsDto { Label = a.CreatedAt.Day.ToString() });
                }
            }


            return Ok(statistics);

        }
        [HttpGet("/activity/user")]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<IEnumerable<AuditDto>>> GetStatistics2()
        {
            var auditEntities = await _auditRepository.GetByMonth();
            var statistics = new List<StatsDto>();
            foreach (Audit a in auditEntities)
            {
                var v = statistics.FindIndex(s => s.Label.Equals(a.UserId.ToString()));
                if (v != -1)
                {
                    statistics.ElementAt(v).Value++;
                }
                else
                {
                    statistics.Add(new StatsDto { Label = a.UserId.ToString() });
                }
            }

            foreach (var s in statistics)
            {
                var t = await _teacherRepository.GetById(new Guid(s.Label));
                if (t != null)
                {
                    s.Label = t.Name;
                }
            }


            return Ok(statistics);

        }
    }
}
