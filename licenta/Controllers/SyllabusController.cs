using AutoMapper;
using licenta.Entities;
using licenta.Models.Syllabuses;
using licenta.Services.InstitutionHierarchy;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using licenta.Services.Teachers;
using licenta.Utils;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/syllabuses")]
    public class SyllabusController : ControllerBase
    {
        private readonly ISection1Repository _section1Repository;
        private readonly ISection2Repository _section2Repository;
        private readonly ISection3Repository _section3Repository;
        private readonly ISection4Repository _section4Repository;
        private readonly ISection5Repository _section5Repository;
        private readonly ISection6Repository _section6Repository;
        private readonly ISection7Repository _section7Repository;
        private readonly ISection8Repository _section8Repository;
        private readonly ISection8ElementRepository _section8ElementRepository;
        private readonly ISection9Repository _section9Repository;
        private readonly ISection10Repository _section10Repository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly ISyllabusTeacherRepository _syllabusTeacherRepository;
        private readonly ISyllabusSubjectRepository _syllabusSubjectRepository;
        private readonly IMapper _mapper;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly ITeacherRepository _teacherRepository;

        public SyllabusController(ISection1Repository section1Repository,
                                  ISection2Repository section2Repository,
                                  ISection3Repository section3Repository,
                                  ISection4Repository section4Repository,
                                  ISection5Repository section5Repository,
                                  ISection6Repository section6Repository,
                                  ISection7Repository section7Repository,
                                  ISection8Repository section8Repository,
                                  ISection8ElementRepository section8ElementRepository,
                                  ISection9Repository section9Repository,
                                  ISection10Repository section10Repository,
        ISubjectRepository subjectRepository,
                                  ISyllabusRepository syllabusRepository,
                                  ISyllabusTeacherRepository syllabusTeacherRepository,
                                  ISyllabusSubjectRepository section4SubjectRepository,
                                  IMapper mapper,
                                  IInstitutionRepository institutionRepository,
                                  IFacultyRepository facultyRepository,
                                  IDepartmentRepository departmentRepository,
                                  IFieldOfStudyRepository fieldOfStudyRepository,
                                  ITeacherRepository teacherRepository)
        {
            _section1Repository = section1Repository ?? throw new ArgumentNullException(nameof(section1Repository));
            _section2Repository = section2Repository ?? throw new ArgumentNullException(nameof(section2Repository));
            _section3Repository = section3Repository ?? throw new ArgumentNullException(nameof(section3Repository));
            _section4Repository = section4Repository ?? throw new ArgumentNullException(nameof(section4Repository));
            _section5Repository = section5Repository ?? throw new ArgumentNullException(nameof(section5Repository));
            _section6Repository = section6Repository ?? throw new ArgumentNullException(nameof(section6Repository));
            _section7Repository = section7Repository ?? throw new ArgumentNullException(nameof(section7Repository));
            _section8Repository = section8Repository ?? throw new ArgumentNullException(nameof(section8Repository));
            _section8ElementRepository = section8ElementRepository ?? throw new ArgumentNullException(nameof(section8ElementRepository));
            _section9Repository = section9Repository ?? throw new ArgumentNullException(nameof(section9Repository));
            _section10Repository = section10Repository ?? throw new ArgumentNullException(nameof(section10Repository));
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
            _syllabusTeacherRepository = syllabusTeacherRepository ?? throw new ArgumentNullException(nameof(syllabusTeacherRepository));
            _syllabusSubjectRepository = section4SubjectRepository ?? throw new ArgumentNullException(nameof(section4SubjectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section1Dto>>> GetAll()
        {
            var teacherEntities = await _section2Repository.GetAll();

            return Ok(_mapper.Map<IEnumerable<Section2Dto>>(teacherEntities));

        }

        [HttpPost]
        public async Task<ActionResult<SyllabusDto>> CreateSyllabus([FromBody] SyllabusCreateDto newSyllabus)
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

            var dbSyllabus = new Syllabus { SubjectId = newSyllabus.SubjectId };
            await _syllabusRepository.CreateSyllabus(dbSyllabus);

            var section1Id = await CreateSection1(newSyllabus.section1, dbSyllabus.Id);
            var section2Id = await CreateSection2(newSyllabus.section2, dbSyllabus.Id);
            var section3Id = await CreateSection3(newSyllabus.section3, dbSyllabus.Id);
            var section4Id = await CreateSection4(newSyllabus.section4, dbSyllabus.Id);
            var section5Id = await CreateSection5(newSyllabus.section5, dbSyllabus.Id);
            var section6Id = await CreateSection6(newSyllabus.section6, dbSyllabus.Id);
            var section7Id = await CreateSection7(newSyllabus.section7, dbSyllabus.Id);
            var section8Id = await CreateSection8(newSyllabus.section8, dbSyllabus.Id);
            var section9Id = await CreateSection9(newSyllabus.section9, dbSyllabus.Id);
            var section10Id = await CreateSection10(newSyllabus.section10, dbSyllabus.Id);
            dbSyllabus.Section1Id = section1Id;
            dbSyllabus.Section2Id = section2Id;
            dbSyllabus.Section3Id = section3Id;
            dbSyllabus.Section4Id = section4Id;
            dbSyllabus.Section5Id = section5Id;
            dbSyllabus.Section6Id = section6Id;
            dbSyllabus.Section7Id = section7Id;
            dbSyllabus.Section8Id = section8Id;
            dbSyllabus.Section9Id = section9Id;
            dbSyllabus.Section10Id = section10Id;
            await _syllabusRepository.SaveChanges();
            var syllabusToReturn = _mapper.Map<SyllabusDto>(dbSyllabus);
            return Ok(syllabusToReturn);

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
        /* private async Task<bool> ValidateSection8(Section2CreateDto newEntry)
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
         }*/

        private async Task<Guid> CreateSection1(Section1CreateDto newEntry, Guid syllabusId)
        {
            var dbSection1 = _mapper.Map<Section1>(newEntry);
            dbSection1.SyllabusId = syllabusId;
            await _section1Repository.CreateSection1(dbSection1);
            return dbSection1.Id;
        }
        private async Task<Guid> CreateSection2(Section2CreateDto newEntry, Guid syllabusId)
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
                    await _syllabusTeacherRepository.CreateSyllabusTeacher(new SyllabusTeacher { TeacherId = id, SyllabusId = syllabusId });

                }
            }

            var dbSection2 = _mapper.Map<Section2>(newEntry);
            dbSection2.SyllabusId = syllabusId;
            await _section2Repository.CreateSection2(dbSection2);
            return dbSection2.Id;
        }
        private async Task<Guid> CreateSection3(Section3CreateDto newEntry, Guid syllabusId)
        {
            var dbSection3 = _mapper.Map<Section3>(newEntry);
            dbSection3.SyllabusId = syllabusId;
            await _section3Repository.CreateSection3(dbSection3);
            return dbSection3.Id;
        }
        private async Task<Guid> CreateSection4(Section4CreateDto newEntry, Guid syllabusId)
        {
            var dbSubjects = await _syllabusSubjectRepository.GetAllBySyllabusId(syllabusId);
            foreach (SyllabusSubject sb in dbSubjects)
            {
                if (!newEntry.Subjects.Contains(sb.SubjectId))
                {
                    _syllabusSubjectRepository.DeleteSyllabusSubject(sb);
                }
            }
            foreach (Guid id in newEntry.Subjects)
            {
                if (!dbSubjects.Any(i => i.SubjectId == id))
                {
                    await _syllabusSubjectRepository.CreateSyllabusSubject(new SyllabusSubject { SyllabusId = syllabusId, SubjectId = id });
                }
            }

            string competences = StringConvertor.MapStringListToString(newEntry.Compentences);

            var dbSection4 = new Section4 { Competences = competences, SyllabusId = syllabusId };
            await _section4Repository.CreateSection4(dbSection4);

            return dbSection4.Id;
        }
        private async Task<Guid> CreateSection5(Section5CreateDto newEntry, Guid syllabusId)
        {
            var dbSection5 = new Section5
            {
                Application = StringConvertor.MapStringListToString(newEntry.Application),
                Course = StringConvertor.MapStringListToString(newEntry.Course),
                SyllabusId = syllabusId,
            };

            await _section5Repository.CreateSection5(dbSection5);
            return dbSection5.Id;
        }
        private async Task<Guid> CreateSection6(Section6CreateDto newEntry, Guid syllabusId)
        {
            var dbSection6 = new Section6
            {
                Professional = StringConvertor.MapStringListToString(newEntry.Professional),
                Cross = StringConvertor.MapStringListToString(newEntry.Cross),
                SyllabusId = syllabusId,
            };

            await _section6Repository.CreateSection6(dbSection6);
            return dbSection6.Id;
        }
        private async Task<Guid> CreateSection7(Section7CreateDto newEntry, Guid syllabusId)
        {
            var dbSection7 = new Section7
            {
                GeneralObjective = newEntry.GeneralObjective,
                SpecificObjectives = StringConvertor.MapStringListToString(newEntry.SpecificObjectives),
                SyllabusId = syllabusId,
            };

            await _section7Repository.CreateSection7(dbSection7);
            return dbSection7.Id;
        }
        private async Task<Guid> CreateSection8(Section8CreateDto newEntry, Guid syllabusId)
        {
            var dbSection8 = new Section8
            {
                TeachingMethodsCourse = StringConvertor.MapStringListToString(newEntry.TeachingMethodsCourse),
                TeachingMethodsLab = StringConvertor.MapStringListToString(newEntry.TeachingMethodsLab),
                BibliographyCourse = StringConvertor.MapStringListToString(newEntry.BibliographyCourse),
                BibliographyLab = StringConvertor.MapStringListToString(newEntry.BibliographyLab),
                SyllabusId = syllabusId,
            };

            await _section8Repository.CreateSection8(dbSection8);

            foreach (Section8ElementCreateDto e in newEntry.LecturesCourse)
            {
                var dbElement = _mapper.Map<Section8Element>(e);
                dbElement.IsCourse = true;
                dbElement.Section8Id = dbSection8.Id;

                await _section8ElementRepository.CreateSection8Element(dbElement);
                await _section8Repository.AddElementToSection8(dbSection8.Id, dbElement);
            }
            foreach (Section8ElementCreateDto e in newEntry.LecturesLab)
            {
                var dbElement = _mapper.Map<Section8Element>(e);
                dbElement.IsCourse = false;
                dbElement.Section8Id = dbSection8.Id;

                await _section8ElementRepository.CreateSection8Element(dbElement);
                await _section8Repository.AddElementToSection8(dbSection8.Id, dbElement);
            }

            return dbSection8.Id;
        }
        private async Task<Guid> CreateSection9(Section9CreateDto newEntry, Guid syllabusId)
        {
            var dbSection9 = _mapper.Map<Section9>(newEntry);
            dbSection9.SyllabusId = syllabusId;
            await _section9Repository.CreateSection9(dbSection9);
            return dbSection9.Id;
        }

        private async Task<Guid> CreateSection10(Section10CreateDto newEntry, Guid syllabusId)
        {
            var dbSection10 = _mapper.Map<Section10>(newEntry);
            dbSection10.SyllabusId = syllabusId;
            dbSection10.ConditionsFinalExam = StringConvertor.MapStringListToString(newEntry.ConditionsFinalExam);

            await _section10Repository.CreateSection10(dbSection10);
            return dbSection10.Id;
        }
    }
}

