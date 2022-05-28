using AutoMapper;
using licenta.Models.Teachers;
using licenta.Services.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers(bool active)
        {
            var teacherEntities = await _teacherRepository.GetAll(active);
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
            var dbTeacher = _mapper.Map<Entities.Teacher>(newTeacher);
            dbTeacher.Role = Entities.Constants.Role.User;
            dbTeacher.Active = true;
            await _teacherRepository.CreateTeacher(dbTeacher);

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

            _mapper.Map(updatedTeacher, oldTeacher);
            await _teacherRepository.SaveChanges();

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
            return Ok();

        }
    }
}
