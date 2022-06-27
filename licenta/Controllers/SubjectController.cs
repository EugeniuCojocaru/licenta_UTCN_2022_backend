using AutoMapper;
using licenta.Entities;
using licenta.Models.Subjects;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, ISyllabusRepository syllabusRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
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
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SubjectDto>(subject));

        }
        [HttpGet("{id}/syllabus")]
        public async Task<ActionResult> GetSyllabus(Guid id)
        {
            var syllabus = await _syllabusRepository.GetBySubjectId(id);
            if (syllabus == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Syllabus>(syllabus));

        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectCreateDto newSubject)
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

        [HttpPut]
        public async Task<ActionResult> UpdateSubject(SubjectUpdateDto updatedSubject)
        {
            var oldSubject = await _subjectRepository.GetById(updatedSubject.Id);
            if (oldSubject == null)
            {
                return NotFound("You must not change the subject's id");
            }
            if (await _subjectRepository.Exists(updatedSubject.Code, updatedSubject.Id))
            {
                return Conflict("Same code not allowed");
            }
            _mapper.Map(updatedSubject, oldSubject);
            await _subjectRepository.SaveChanges();

            return Ok();
        }

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
