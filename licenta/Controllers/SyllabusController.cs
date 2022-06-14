using AutoMapper;
using licenta.Entities;
using licenta.Models.Syllabuses;
using licenta.Services.InstitutionHierarchy;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using licenta.Services.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/syllabuses")]
    public class SyllabusController : ControllerBase
    {
        private readonly ISection1Repository _section1Repository;
        private readonly ISection2Repository _section2Repository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly ISyllabusTeacherRepository _syllabusTeacherRepository;

        public SyllabusController(
            ISection1Repository section1Repository,
            ISection2Repository section2Repository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IMapper mapper,
            IInstitutionRepository institutionRepository,
            IFacultyRepository facultyRepository,
            IDepartmentRepository departmentRepository,
            IFieldOfStudyRepository fieldOfStudyRepository,
            ISyllabusTeacherRepository syllabusTeacherRepository)
        {
            _section1Repository = section1Repository ?? throw new ArgumentNullException(nameof(section1Repository));
            _section2Repository = section2Repository ?? throw new ArgumentNullException(nameof(section2Repository));
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _syllabusTeacherRepository = syllabusTeacherRepository ?? throw new ArgumentNullException(nameof(syllabusTeacherRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section1Dto>>> GetAll()
        {
            var teacherEntities = await _section2Repository.GetAll();

            return Ok(_mapper.Map<IEnumerable<Section2Dto>>(teacherEntities));

        }

        [HttpPost]
        public async Task<ActionResult> CreateSyllabus([FromBody] SyllabusCreateDto newSyllabus)
        {
            if (await ValidateSection1(newSyllabus.section1) == false)
                return NotFound("Error when validating section 1");
            if (await ValidateSection2(newSyllabus.section2) == false)
                return NotFound("Error when validating section 2");

            var subject = await _subjectRepository.GetById(newSyllabus.SubjectId);
            if (subject == null)
            {
                return NotFound("Could not find subject");
            }



            return Ok("caca");

        }

        private async Task<bool> ValidateSection1(Section1CreateDto newEntry)
        {
            if (newEntry == null) return false;
            if (await _institutionRepository.Exists(newEntry.InstitutionId) == false)
                return false;
            if (await _facultyRepository.Exists(newEntry.FacultyId) == false)
                return false;
            if (await _departmentRepository.Exists(newEntry.DepartmentId) == false)
                return false;
            if (await _fieldOfStudyRepository.Exists(newEntry.FieldOfStudyId) == false)
                return false;

            return true;
        }
        private async Task<bool> ValidateSection2(Section2CreateDto newEntry)
        {
            if (newEntry == null) return false;
            if (await _teacherRepository.Exists(newEntry.TeacherId) == false)
                return false;

            if (newEntry.Teachers.Count == 0)
                return false;

            foreach (Guid id in newEntry.Teachers)
            {
                if (await _teacherRepository.Exists(id) == false)
                    return false;
            }

            return true;
        }
        private async Task<bool> CreateSection2(Section2CreateDto newEntry, Guid syllabusId)
        {
            var dbTeachers = await _syllabusTeacherRepository.GetAllBySyllabusId(syllabusId);
            foreach (SyllabusTeacher sb in dbTeachers)
            {
                if (!newEntry.Teachers.Contains(sb.TeacherId))
                {
                    _syllabusTeacherRepository.DeleteSyllabusTeacher(sb);
                }
            }
            foreach (Guid id in newEntry.Teachers)
            {
                if (!dbTeachers.Any(i => i.TeacherId == id))
                {
                    if (await _syllabusTeacherRepository.CreateSyllabusTeacher(new SyllabusTeacher { TeacherId = id, SyllabusId = syllabusId }) == false)
                        return false;
                }
            }

            var dbSection2 = _mapper.Map<Section2>(newEntry);
            await _section2Repository.CreateSection2(dbSection2);
            return true;
        }

    }
}

