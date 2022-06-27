using AutoMapper;
using licenta.Entities;
using licenta.Models.Teachers;
using licenta.Services.Audits;
using licenta.Services.Teachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers(bool active, bool? list = false)
        {
            var teacherEntities = await _teacherRepository.GetAll(active);
            if (list == true)
            {
                Ok(_mapper.Map<IEnumerable<TeacherListDto>>(teacherEntities));
            }
            return Ok(_mapper.Map<IEnumerable<TeacherDto>>(teacherEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeacher(Guid id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TeacherDto>(teacher));

        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> CreateTeacher(TeacherCreateDto newTeacher)
        {
            if (await _teacherRepository.Exists(newTeacher.Email))
            {
                return Conflict("Same email not allowed");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newTeacher.Password);
            var dbTeacher = _mapper.Map<Entities.Teacher>(newTeacher);
            dbTeacher.Password = hashedPassword;
            dbTeacher.Role = Entities.Constants.Role.User;
            dbTeacher.Active = true;
            await _teacherRepository.CreateTeacher(dbTeacher);

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Teacher,
            };
            await _auditRepository.CreateAudit(dbAudit);

            var teacherToReturn = _mapper.Map<TeacherDto>(dbTeacher);
            return Ok(teacherToReturn);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeacher(TeacherUpdateDto updatedTeacher)
        {
            var oldTeacher = await _teacherRepository.GetById(updatedTeacher.Id);
            if (oldTeacher == null)
            {
                return NotFound();
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Teacher,
                Notes = oldTeacher.Name + " -> " + updatedTeacher.Name,
            };


            _mapper.Map(updatedTeacher, oldTeacher);
            await _teacherRepository.SaveChanges();
            await _auditRepository.CreateAudit(dbAudit);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTeacher(Guid id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound("Teacher does not exist");
            }

            teacher.Active = false;

            await _teacherRepository.SaveChanges();

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Teacher,
                Notes = teacher.Name
            };
            await _auditRepository.CreateAudit(dbAudit);
            return Ok();

        }
    }
}
