using AutoMapper;
using licenta.Models.Subjects;
using licenta.Services.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var teacherEntities = await _subjectRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SubjectDto>>(teacherEntities));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubject(Guid id)
        {
            var teacher = await _subjectRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SubjectDto>(teacher));

        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateTeacher(SubjectCreateDto newSubject)
        {
            if (await _subjectRepository.Exists(newSubject.Code))
            {
                return Conflict("Same code not allowed");
            }
            var dbSubject = _mapper.Map<Entities.Subject>(newSubject);
            await _subjectRepository.CreateSubject(dbSubject);

            var subjectToReturn = _mapper.Map<SubjectDto>(dbSubject);
            return Ok(subjectToReturn);

        }

        //[HttpPut]
        //public async Task<ActionResult> UpdateTeacher(TeacherUpdateDto updatedTeacher)
        //{
        //    var oldTeacher = await _subjectRepository.GetById(updatedTeacher.Id);
        //    if (oldTeacher == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(updatedTeacher, oldTeacher);
        //    await _subjectRepository.SaveChanges();

        //    return Ok();
        //}

        [HttpDelete]
        public async Task<ActionResult> DeleteSubject(Guid id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound("Subject does not exist");
            }
            _subjectRepository.DeleteSubject(subject);
            await _subjectRepository.SaveChanges();
            return Ok();

        }
    }
}
