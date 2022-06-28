using AutoMapper;
using licenta.Entities;
using licenta.Models.Subjects;
using licenta.Models.Syllabuses;
using licenta.Models.Teachers;
using licenta.Services.Audits;
using licenta.Services.InstitutionHierarchy;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using licenta.Services.Teachers;
using licenta.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Authorize]
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
        private readonly ISyllabusVersionRepository _syllabusVersionRepository;
        private readonly IMapper _mapper;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuditRepository _auditRepository;

        public SyllabusController(ISection1Repository section1Repository, ISection2Repository section2Repository, ISection3Repository section3Repository, ISection4Repository section4Repository, ISection5Repository section5Repository, ISection6Repository section6Repository, ISection7Repository section7Repository, ISection8Repository section8Repository, ISection8ElementRepository section8ElementRepository, ISection9Repository section9Repository, ISection10Repository section10Repository, ISubjectRepository subjectRepository, ISyllabusRepository syllabusRepository, ISyllabusTeacherRepository syllabusTeacherRepository, ISyllabusSubjectRepository syllabusSubjectRepository, ISyllabusVersionRepository syllabusVersionRepository, IMapper mapper, IInstitutionRepository institutionRepository, IFacultyRepository facultyRepository, IDepartmentRepository departmentRepository, IFieldOfStudyRepository fieldOfStudyRepository, ITeacherRepository teacherRepository, IAuditRepository auditRepository)
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
            _syllabusSubjectRepository = syllabusSubjectRepository ?? throw new ArgumentNullException(nameof(syllabusSubjectRepository));
            _syllabusVersionRepository = syllabusVersionRepository ?? throw new ArgumentNullException(nameof(syllabusVersionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SyllabusDto>>> GetAll()
        {
            var syllabusEntities = await _syllabusRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SyllabusDto>>(syllabusEntities));
        }
        [HttpGet("subject/{id}")]
        public async Task<ActionResult<SyllabusDto>> GetSyllabusBySubjectId(Guid id)
        {
            var syllabusVersion = await _syllabusVersionRepository.GetBySubjectId(id);
            var syllabus = await _syllabusRepository.GetById(syllabusVersion.SyllabusId);
            if (syllabus == null)
            {
                return NotFound();
            }
            var syllabusDto = await MapSyllabusToSyllabusDto(syllabus);
            return Ok(syllabusDto);
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateSyllabus(SyllabusCreateDto newSyllabus, Guid oldSyllabusId)
        {
            // verificare existenta fisa disciplina veche
            var oldSyllabus = await _syllabusRepository.GetById(oldSyllabusId);
            if (oldSyllabus == null)
                return NotFound("Could not find syllabus to update");
            //validare sectiouni
            if (await ValidateSection1(newSyllabus.section1) == false)
                return NotFound("Error when validating section 1");
            if (await ValidateSection2(newSyllabus.section2) == false)
                return NotFound("Error when validating section 2");
            // validare materie
            var subject = await _subjectRepository.GetById(newSyllabus.SubjectId);
            if (subject == null)
            {
                return NotFound("Could not find subject");
            }
            //creare syllabus
            var dbSyllabus = new Syllabus { SubjectId = newSyllabus.SubjectId };
            await _syllabusRepository.CreateSyllabus(dbSyllabus);

            var note = "Changed sections: ";
            // creare sectiunea 1 si verificare daca sunt aceleasi
            var section1Result = await SameSection1(oldSyllabus.Section1Id, newSyllabus.section1, dbSyllabus.Id);
            dbSyllabus.Section1Id = section1Result.Item1;
            note += section1Result.Item2;
            // creare sectiunea 2 si verificare daca sunt aceleasi
            var section2Result = await SameSection2(oldSyllabus.Section2Id, newSyllabus.section2, dbSyllabus.Id);
            dbSyllabus.Section2Id = section2Result.Item1;
            note += section2Result.Item2;
            // creare sectiunea 3 si verificare daca sunt aceleasi
            var section3Result = await SameSection3(oldSyllabus.Section3Id, newSyllabus.section3, dbSyllabus.Id);
            dbSyllabus.Section3Id = section3Result.Item1;
            note += section3Result.Item2;
            // creare sectiunea 4 si verificare daca sunt aceleasi
            var section4Result = await SameSection4(oldSyllabus.Section4Id, newSyllabus.section4, dbSyllabus.Id);
            dbSyllabus.Section4Id = section4Result.Item1;
            note += section4Result.Item2;
            // creare sectiunea 5 si verificare daca sunt aceleasi
            var section5Result = await SameSection5(oldSyllabus.Section5Id, newSyllabus.section5, dbSyllabus.Id);
            dbSyllabus.Section5Id = section5Result.Item1;
            note += section5Result.Item2;
            // creare sectiunea 6 si verificare daca sunt aceleasi
            var section6Result = await SameSection6(oldSyllabus.Section6Id, newSyllabus.section6, dbSyllabus.Id);
            dbSyllabus.Section6Id = section6Result.Item1;
            note += section6Result.Item2;
            // creare sectiunea 7 si verificare daca sunt aceleasi
            var section7Result = await SameSection7(oldSyllabus.Section7Id, newSyllabus.section7, dbSyllabus.Id);
            dbSyllabus.Section7Id = section7Result.Item1;
            note += section7Result.Item2;
            // creare sectiunea 8 si verificare daca sunt aceleasi
            var section8Result = await SameSection8(oldSyllabus.Section8Id, newSyllabus.section8, dbSyllabus.Id);
            dbSyllabus.Section8Id = section8Result.Item1;
            note += section8Result.Item2;
            // creare sectiunea 9 si verificare daca sunt aceleasi
            var section9Result = await SameSection9(oldSyllabus.Section9Id, newSyllabus.section9, dbSyllabus.Id);
            dbSyllabus.Section9Id = section9Result.Item1;
            note += section9Result.Item2;
            // creare sectiunea 10 si verificare daca sunt aceleasi
            var section10Result = await SameSection10(oldSyllabus.Section10Id, newSyllabus.section10, dbSyllabus.Id);
            dbSyllabus.Section10Id = section10Result.Item1;
            note += section10Result.Item2;
            if (note.Equals("Changed sections: "))
            {
                _syllabusRepository.DeleteSyllabus(dbSyllabus);
                return Ok("Nothing to update");
            }

            await _syllabusRepository.SaveChanges();
            var oldSyllabusVersion = await _syllabusVersionRepository.GetBySubjectId(newSyllabus.SubjectId);
            oldSyllabusVersion.UpdatedAt = DateTime.UtcNow;
            await _syllabusVersionRepository.SaveChanges();

            var newSyllabusVersion = new SyllabusVersion
            {
                SyllabusId = dbSyllabus.Id
            };
            await _syllabusVersionRepository.CreateSyllabusVersion(newSyllabusVersion);

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Syllabus,
                Notes = note,
            };
            await _auditRepository.CreateAudit(dbAudit);
            return Ok(note);


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
            if (await _syllabusVersionRepository.SubjectHasSyllabus(newSyllabus.SubjectId))
            {
                return BadRequest("Subject already has a syllabus");
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

            var syllabusVersion = new SyllabusVersion
            {
                SyllabusId = dbSyllabus.Id
            };
            await _syllabusVersionRepository.CreateSyllabusVersion(syllabusVersion);
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Syllabus,
                Notes = subject.Name
            };
            await _auditRepository.CreateAudit(dbAudit);
            var syllabusToReturn = await MapSyllabusToSyllabusDto(dbSyllabus);
            return Ok(syllabusToReturn);

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSyllabus(Guid subjectId)
        {
            var subject = await _subjectRepository.GetById(subjectId);
            if (subject == null)
            {
                return NotFound("Subject does not exist");
            }
            var oldSyllabusVersion = await _syllabusVersionRepository.GetBySubjectId(subjectId);
            oldSyllabusVersion.UpdatedAt = DateTime.UtcNow;
            await _syllabusVersionRepository.SaveChanges();
            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;

            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Syllabus,
                Notes = subject.Name
            };
            await _auditRepository.CreateAudit(dbAudit);
            return Ok();

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

            string competences = StringConvertor.MapStringListToString(newEntry.Competences);

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

        private async Task<SyllabusDto> MapSyllabusToSyllabusDto(Syllabus syllabus)
        {
            var subject = await _subjectRepository.GetById(syllabus.SubjectId);
            var s1Dto = await Section1ToDto(syllabus.Section1Id);
            var s2Dto = await Section2ToDto(syllabus.Section2Id, syllabus.Id);
            var s3Dto = await Section3ToDto(syllabus.Section3Id);
            var s4Dto = await Section4ToDto(syllabus.Section4Id, syllabus.Id);
            var s5Dto = await Section5ToDto(syllabus.Section5Id);
            var s6Dto = await Section6ToDto(syllabus.Section6Id);
            var s7Dto = await Section7ToDto(syllabus.Section7Id);
            var s8Dto = await Section8ToDto(syllabus.Section8Id);
            var s9Dto = await Section9ToDto(syllabus.Section9Id);
            var s10Dto = await Section10ToDto(syllabus.Section10Id);

            var syllabusDto = new SyllabusDto
            {
                Id = syllabus.Id,
                Subject = _mapper.Map<SubjectDto>(subject),
                Section1 = s1Dto,
                Section2 = s2Dto,
                Section3 = s3Dto,
                Section4 = s4Dto,
                Section5 = s5Dto,
                Section6 = s6Dto,
                Section7 = s7Dto,
                Section8 = s8Dto,
                Section9 = s9Dto,
                Section10 = s10Dto,
            };

            return syllabusDto;
        }

        private async Task<Section1Dto> Section1ToDto(Guid? id)
        {
            var section1Db = await _section1Repository.GetById(id);
            var section1Dto = _mapper.Map<Section1Dto>(section1Db);
            return section1Dto;
        }
        private async Task<Section2Dto> Section2ToDto(Guid? id, Guid syllabusId)
        {
            var sDb = await _section2Repository.GetById(id);
            var sDto = _mapper.Map<Section2Dto>(sDb);
            var lecturer = await _teacherRepository.GetById(sDb.TeacherId);
            var lecturerDto = _mapper.Map<TeacherDto>(lecturer);
            var teacherIds = await _syllabusTeacherRepository.GetAllBySyllabusId(syllabusId);
            List<TeacherDto> teachers = new();
            foreach (var teacherId in teacherIds)
            {
                var teacherDb = await _teacherRepository.GetById(teacherId.TeacherId);
                var teacherDto = _mapper.Map<TeacherDto>(teacherDb);
                teachers.Add(teacherDto);
            }
            sDto.Teacher = lecturerDto;
            sDto.Teachers = teachers;

            return sDto;
        }
        private async Task<Section3Dto> Section3ToDto(Guid? id)
        {
            var sDb = await _section3Repository.GetById(id);
            var sDto = _mapper.Map<Section3Dto>(sDb);
            return sDto;
        }
        private async Task<Section4Dto> Section4ToDto(Guid? id, Guid syllabusId)
        {
            var sDb = await _section4Repository.GetById(id);
            var subjectIds = await _syllabusSubjectRepository.GetAllBySyllabusId(syllabusId);
            List<SubjectDto> subjects = new();
            foreach (var subjectId in subjectIds)
            {
                var subjectDb = await _subjectRepository.GetById(subjectId.SubjectId);
                var subjectDto = _mapper.Map<SubjectDto>(subjectDb);
                subjects.Add(subjectDto);
            }
            var sDto = new Section4Dto
            {
                Id = sDb.Id,
                Compentences = StringConvertor.MapStringToStringList(sDb.Competences),
                Subjects = subjects,
            };

            return sDto;
        }
        private async Task<Section5Dto> Section5ToDto(Guid? id)
        {
            var sDb = await _section5Repository.GetById(id);
            var sDto = new Section5Dto
            {
                Id = sDb.Id,
                Application = StringConvertor.MapStringToStringList(sDb.Application),
                Course = StringConvertor.MapStringToStringList(sDb.Course),
            };
            return sDto;
        }
        private async Task<Section6Dto> Section6ToDto(Guid? id)
        {
            var sDb = await _section6Repository.GetById(id);
            var sDto = new Section6Dto
            {
                Id = sDb.Id,
                Professional = StringConvertor.MapStringToStringList(sDb.Professional),
                Cross = StringConvertor.MapStringToStringList(sDb.Cross),
            };
            return sDto;
        }
        private async Task<Section7Dto> Section7ToDto(Guid? id)
        {
            var sDb = await _section7Repository.GetById(id);
            var sDto = new Section7Dto
            {
                Id = sDb.Id,
                GeneralObjective = sDb.GeneralObjective,
                SpecificObjectives = StringConvertor.MapStringToStringList(sDb.SpecificObjectives),
            };
            return sDto;
        }
        private async Task<Section8Dto> Section8ToDto(Guid? id)
        {
            var sDb = await _section8Repository.GetById(id);
            var lectures = await _section8ElementRepository.GetAllBySection8Id(sDb.Id);
            var lecturesCourse = new List<Section8ElementDto>();
            var lecturesLab = new List<Section8ElementDto>();

            foreach (var lecture in lectures)
            {
                if (lecture.IsCourse)
                {
                    lecturesCourse.Add(_mapper.Map<Section8ElementDto>(lecture));
                }
                else
                {
                    lecturesLab.Add(_mapper.Map<Section8ElementDto>(lecture));
                }
            }

            var sDto = new Section8Dto
            {
                Id = sDb.Id,
                TeachingMethodsCourse = StringConvertor.MapStringToStringList(sDb.TeachingMethodsCourse),
                TeachingMethodsLab = StringConvertor.MapStringToStringList(sDb.TeachingMethodsLab),
                BibliographyCourse = StringConvertor.MapStringToStringList(sDb.BibliographyCourse),
                BibliographyLab = StringConvertor.MapStringToStringList(sDb.BibliographyLab),
                LecturesCourse = lecturesCourse,
                LecturesLab = lecturesLab
            };
            return sDto;
        }
        private async Task<Section9Dto> Section9ToDto(Guid? id)
        {
            var sDb = await _section9Repository.GetById(id);
            var sDto = _mapper.Map<Section9Dto>(sDb);
            return sDto;
        }
        private async Task<Section10Dto> Section10ToDto(Guid? id)
        {
            var sDb = await _section10Repository.GetById(id);
            var sDto = _mapper.Map<Section10Dto>(sDb);
            sDto.ConditionsFinalExam = StringConvertor.MapStringToStringList(sDb.ConditionsFinalExam);
            return sDto;
        }

        private async Task<(Guid, string)> SameSection1(Guid? oldSection1Id, Section1CreateDto newSection1CreateDto, Guid newSyllabusId)
        {
            var section1Id = await CreateSection1(newSection1CreateDto, newSyllabusId);

            var oldSection1 = await _section1Repository.GetById(oldSection1Id);
            var newSection1 = await _section1Repository.GetById(section1Id);

            if (oldSection1.InstitutionId != newSection1.InstitutionId) return (newSection1.Id, " section1,");
            if (oldSection1.FacultyId != newSection1.FacultyId) return (newSection1.Id, " section1,");
            if (oldSection1.DepartmentId != newSection1.DepartmentId) return (newSection1.Id, " section1,");
            if (oldSection1.FieldOfStudyId != newSection1.FieldOfStudyId) return (newSection1.Id, " section1,");

            if (oldSection1.CycleOfStudy != newSection1.CycleOfStudy) return (newSection1.Id, " section1,");
            if (oldSection1.ProgramOfStudy != newSection1.ProgramOfStudy) return (newSection1.Id, " section1,");
            if (oldSection1.Qualification != newSection1.Qualification) return (newSection1.Id, " section1,");
            if (oldSection1.FormOfEducation != newSection1.FormOfEducation) return (newSection1.Id, " section1,");

            _section1Repository.DeleteSection1(newSection1);

            return (oldSection1.Id, "");
        }
        private async Task<(Guid, string)> SameSection2(Guid? oldSection2Id, Section2CreateDto newSection2CreateDto, Guid newSyllabusId)
        {
            var section2Id = await CreateSection2(newSection2CreateDto, newSyllabusId);

            var oldSection2 = await _section2Repository.GetById(oldSection2Id);
            var newSection2 = await _section2Repository.GetById(section2Id);

            if (oldSection2.YearOfStudy != newSection2.YearOfStudy) return (newSection2.Id, " section2,");
            if (oldSection2.Semester != newSection2.Semester) return (newSection2.Id, " section2,");
            if (oldSection2.Assessment != newSection2.Assessment) return (newSection2.Id, " section2,");
            if (oldSection2.Category1 != newSection2.Category1) return (newSection2.Id, " section2,");

            if (oldSection2.Category2 != newSection2.Category2) return (newSection2.Id, " section2,");
            if (oldSection2.TeacherId != newSection2.TeacherId) return (newSection2.Id, " section2,");

            var teachersOldSyllabus = await _syllabusTeacherRepository.GetAllBySyllabusId(oldSection2.SyllabusId);
            var teachersNewSyllabus = await _syllabusTeacherRepository.GetAllBySyllabusId(newSyllabusId);

            if (teachersOldSyllabus.Count() != teachersNewSyllabus.Count()) return (newSection2.Id, " section2,");
            else
            {
                foreach (var teacher in teachersOldSyllabus)
                {
                    if (!teachersNewSyllabus.Any(tns => tns.TeacherId == teacher.TeacherId))
                    {
                        return (newSection2.Id, " section2,");
                    }
                }
            }

            _syllabusTeacherRepository.DeleteAllBySyllabusId(newSyllabusId);
            _section2Repository.DeleteSection2(newSection2);

            return (oldSection2.Id, "");
        }
        private async Task<(Guid, string)> SameSection3(Guid? oldSection3Id, Section3CreateDto newSection3CreateDto, Guid newSyllabusId)
        {
            var section3Id = await CreateSection3(newSection3CreateDto, newSyllabusId);

            var oldSection3 = await _section3Repository.GetById(oldSection3Id);
            var newSection3 = await _section3Repository.GetById(section3Id);

            if (oldSection3.CourseHoursPerWeek != newSection3.CourseHoursPerWeek) return (newSection3.Id, " section3,");
            if (oldSection3.SeminarHoursPerWeek != newSection3.SeminarHoursPerWeek) return (newSection3.Id, " section3,");
            if (oldSection3.LaboratoryHoursPerWeek != newSection3.LaboratoryHoursPerWeek) return (newSection3.Id, " section3,");
            if (oldSection3.ProjectHoursPerWeek != newSection3.ProjectHoursPerWeek) return (newSection3.Id, " section3,");

            if (oldSection3.CourseHoursPerSemester != newSection3.CourseHoursPerSemester) return (newSection3.Id, " section3,");
            if (oldSection3.SeminarHoursPerSemester != newSection3.SeminarHoursPerSemester) return (newSection3.Id, " section3,");
            if (oldSection3.LaboratoryHoursPerSemester != newSection3.LaboratoryHoursPerSemester) return (newSection3.Id, " section3,");
            if (oldSection3.ProjectHoursPerSemester != newSection3.ProjectHoursPerSemester) return (newSection3.Id, " section3,");

            if (oldSection3.IndividualStudyA != newSection3.IndividualStudyA) return (newSection3.Id, " section3,");
            if (oldSection3.IndividualStudyB != newSection3.IndividualStudyB) return (newSection3.Id, " section3,");
            if (oldSection3.IndividualStudyC != newSection3.IndividualStudyC) return (newSection3.Id, " section3,");
            if (oldSection3.IndividualStudyD != newSection3.IndividualStudyD) return (newSection3.Id, " section3,");
            if (oldSection3.IndividualStudyE != newSection3.IndividualStudyE) return (newSection3.Id, " section3,");
            if (oldSection3.IndividualStudyF != newSection3.IndividualStudyF) return (newSection3.Id, " section3,");

            if (oldSection3.Credits != newSection3.Credits) return (newSection3.Id, " section3,");

            _section3Repository.DeleteSection3(newSection3);

            return (oldSection3.Id, "");
        }
        private async Task<(Guid, string)> SameSection4(Guid? oldSection4Id, Section4CreateDto newSection4CreateDto, Guid newSyllabusId)
        {
            var section4Id = await CreateSection4(newSection4CreateDto, newSyllabusId);

            var oldSection4 = await _section4Repository.GetById(oldSection4Id);
            var newSection4 = await _section4Repository.GetById(section4Id);

            if (oldSection4.Competences != newSection4.Competences) return (newSection4.Id, " section4,");

            var subjectsOldSyllabus = await _syllabusSubjectRepository.GetAllBySyllabusId(oldSection4.SyllabusId);
            var subjectsNewSyllabus = await _syllabusSubjectRepository.GetAllBySyllabusId(newSyllabusId);

            if (subjectsOldSyllabus.Count() != subjectsNewSyllabus.Count()) return (newSection4.Id, " section4,");
            else
            {
                foreach (var subject in subjectsOldSyllabus)
                {
                    if (!subjectsNewSyllabus.Any(sns => sns.SubjectId == subject.SubjectId))
                    {
                        return (newSection4.Id, " section4,");
                    }
                }
            }
            _syllabusSubjectRepository.DeleteAllBySyllabusId(newSyllabusId);
            _section4Repository.DeleteSection4(newSection4);

            return (oldSection4.Id, "");
        }
        private async Task<(Guid, string)> SameSection5(Guid? oldSection5Id, Section5CreateDto newSection5CreateDto, Guid newSyllabusId)
        {
            var section5Id = await CreateSection5(newSection5CreateDto, newSyllabusId);

            var oldSection5 = await _section5Repository.GetById(oldSection5Id);
            var newSection5 = await _section5Repository.GetById(section5Id);

            if (oldSection5.Course != newSection5.Course) return (newSection5.Id, " section5,");
            if (oldSection5.Application != newSection5.Application) return (newSection5.Id, " section5,");

            _section5Repository.DeleteSection5(newSection5);

            return (oldSection5.Id, "");
        }
        private async Task<(Guid, string)> SameSection6(Guid? oldSection6Id, Section6CreateDto newSection6CreateDto, Guid newSyllabusId)
        {
            var section6Id = await CreateSection6(newSection6CreateDto, newSyllabusId);

            var oldSection6 = await _section6Repository.GetById(oldSection6Id);
            var newSection6 = await _section6Repository.GetById(section6Id);

            if (oldSection6.Professional != newSection6.Professional) return (newSection6.Id, " section6,");
            if (oldSection6.Cross != newSection6.Cross) return (newSection6.Id, " section6,");

            _section6Repository.DeleteSection6(newSection6);

            return (oldSection6.Id, "");
        }
        private async Task<(Guid, string)> SameSection7(Guid? oldSection7Id, Section7CreateDto newSection7CreateDto, Guid newSyllabusId)
        {
            var section7Id = await CreateSection7(newSection7CreateDto, newSyllabusId);

            var oldSection7 = await _section7Repository.GetById(oldSection7Id);
            var newSection7 = await _section7Repository.GetById(section7Id);

            if (oldSection7.GeneralObjective != newSection7.GeneralObjective) return (newSection7.Id, " section7,");
            if (oldSection7.SpecificObjectives != newSection7.SpecificObjectives) return (newSection7.Id, " section7,");

            _section7Repository.DeleteSection7(newSection7);

            return (oldSection7.Id, "");
        }
        private async Task<(Guid, string)> SameSection8(Guid? oldSection8Id, Section8CreateDto newSection8CreateDto, Guid newSyllabusId)
        {
            var section8Id = await CreateSection8(newSection8CreateDto, newSyllabusId);

            var oldSection8 = await _section8Repository.GetById(oldSection8Id);
            var newSection8 = await _section8Repository.GetById(section8Id);

            if (oldSection8.TeachingMethodsCourse != newSection8.TeachingMethodsCourse) return (newSection8.Id, " section8,");
            if (oldSection8.TeachingMethodsLab != newSection8.TeachingMethodsLab) return (newSection8.Id, " section8,");
            if (oldSection8.BibliographyCourse != newSection8.BibliographyCourse) return (newSection8.Id, " section8,");
            if (oldSection8.BibliographyLab != newSection8.BibliographyLab) return (newSection8.Id, " section8,");


            var lecturesOldSection8 = await _section8ElementRepository.GetAllBySection8Id(oldSection8.Id);
            var lecturesNewSection8 = await _section8ElementRepository.GetAllBySection8Id(newSection8.Id);

            if (lecturesOldSection8.Count() != lecturesNewSection8.Count()) return (newSection8.Id, " section8,");
            else
            {
                foreach (var lecture in lecturesOldSection8)
                {
                    if (!lecturesNewSection8.Any(lns => lns.Name == lecture.Name && lns.Duration == lecture.Duration && lns.IsCourse == lecture.IsCourse && lns.Note == lecture.Note))
                    {
                        return (newSection8.Id, " section8,");
                    }
                }
            }
            _section8ElementRepository.DeleteAllBySection8Id(newSection8.Id);
            _section8Repository.DeleteSection8(newSection8);

            return (oldSection8.Id, "");
        }
        private async Task<(Guid, string)> SameSection9(Guid? oldSection9Id, Section9CreateDto newSection9CreateDto, Guid newSyllabusId)
        {
            var section9Id = await CreateSection9(newSection9CreateDto, newSyllabusId);

            var oldSection9 = await _section9Repository.GetById(oldSection9Id);
            var newSection9 = await _section9Repository.GetById(section9Id);

            if (oldSection9.Description != newSection9.Description) return (newSection9.Id, " section9,");

            _section9Repository.DeleteSection9(newSection9);

            return (oldSection9.Id, "");
        }
        private async Task<(Guid, string)> SameSection10(Guid? oldSection10Id, Section10CreateDto newSection10CreateDto, Guid newSyllabusId)
        {
            var section10Id = await CreateSection10(newSection10CreateDto, newSyllabusId);

            var oldSection10 = await _section10Repository.GetById(oldSection10Id);
            var newSection10 = await _section10Repository.GetById(section10Id);

            if (oldSection10.CourseCriteria != newSection10.CourseCriteria) return (newSection10.Id, " section10,");
            if (oldSection10.CourseMethods != newSection10.CourseMethods) return (newSection10.Id, " section10,");
            if (oldSection10.CourcePercentage != newSection10.CourcePercentage) return (newSection10.Id, " section10,");
            if (oldSection10.SeminarCriteria != newSection10.SeminarCriteria) return (newSection10.Id, " section10,");
            if (oldSection10.SeminarMethods != newSection10.SeminarMethods) return (newSection10.Id, " section10,");
            if (oldSection10.SeminarPercentage != newSection10.SeminarPercentage) return (newSection10.Id, " section10,");
            if (oldSection10.LaboratoryCriteria != newSection10.LaboratoryCriteria) return (newSection10.Id, " section10,");
            if (oldSection10.LaboratoryMethods != newSection10.LaboratoryMethods) return (newSection10.Id, " section10,");
            if (oldSection10.LaboratoryPercentage != newSection10.LaboratoryPercentage) return (newSection10.Id, " section10,");
            if (oldSection10.ProjectCriteria != newSection10.ProjectCriteria) return (newSection10.Id, " section10,");
            if (oldSection10.ProjectMethods != newSection10.ProjectMethods) return (newSection10.Id, " section10,");
            if (oldSection10.ProjectPercentage != newSection10.ProjectPercentage) return (newSection10.Id, " section10,");
            if (oldSection10.MinimumPerformance != newSection10.MinimumPerformance) return (newSection10.Id, " section10,");
            if (oldSection10.ConditionsFinalExam != newSection10.ConditionsFinalExam) return (newSection10.Id, " section10,");
            if (oldSection10.ConditionPromotion != newSection10.ConditionPromotion) return (newSection10.Id, " section10,");

            _section10Repository.DeleteSection10(newSection10);

            return (oldSection10.Id, "");
        }
    }
}

