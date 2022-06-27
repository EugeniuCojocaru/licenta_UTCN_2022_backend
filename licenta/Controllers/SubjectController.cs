﻿using AutoMapper;
using licenta.Entities;
using licenta.Models.Subjects;
using licenta.Services.Audits;
using licenta.Services.Subjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IAuditRepository auditRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
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

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectCreateDto newSubject)
        {
            if (await _subjectRepository.Exists(newSubject.Code))
            {
                return Conflict("Same code not allowed");
            }
            var dbSubject = _mapper.Map<Entities.Subject>(newSubject);
            await _subjectRepository.CreateSubject(dbSubject);

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.CREATE,
                Entity = Constants.EntityNames.Subject,
            };
            await _auditRepository.CreateAudit(dbAudit);

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

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.UPDATE,
                Entity = Constants.EntityNames.Subject,
                Notes = oldSubject.Name + " -> " + updatedSubject.Name,
            };

            _mapper.Map(updatedSubject, oldSubject);
            await _subjectRepository.SaveChanges();
            await _auditRepository.CreateAudit(dbAudit);
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

            var userId = User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.Value;
            var dbAudit = new Audit
            {
                UserId = Guid.Parse(userId),
                Operation = Constants.Operation.DELETE,
                Entity = Constants.EntityNames.Subject,
                Notes = subject.Name
            };
            await _auditRepository.CreateAudit(dbAudit);

            return Ok();

        }
    }
}
