﻿using AutoMapper;
using licenta.Models.InstitutionHierarchy;
using licenta.Services.InstitutionHierarchy;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository, IFieldOfStudyRepository fieldOfStudyRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentWithoutFieldOfStudyDto>>> GetDepartments()
        {
            var departmentEntities = await _departmentRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<DepartmentWithoutFieldOfStudyDto>>(departmentEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(Guid departmentId, bool includeFiledsOfStudy = false)
        {
            var department = await _departmentRepository.GetById(departmentId);
            if (department == null)
            {
                return NotFound();
            }
            if (includeFiledsOfStudy)
            {
                return Ok(_mapper.Map<DepartmentDto>(department));
            }
            return Ok(_mapper.Map<DepartmentWithoutFieldOfStudyDto>(department));

        }

        [HttpGet("{departmentId}/fieldsOfStudy")]
        public async Task<ActionResult> GetFieldsOfStudyByDepartmentId(Guid departmentId)
        {
            var department = await _departmentRepository.GetById(departmentId);

            if (department == null)
            {
                return NotFound();
            }
            var fieldsOfStudyEntities = await _fieldOfStudyRepository.GetAllByDepartmentId(departmentId);

            return Ok(_mapper.Map<IEnumerable<FieldOfStudyDto>>(fieldsOfStudyEntities));

        }

        [HttpPost]
        public async Task<ActionResult<DepartmentWithoutFieldOfStudyDto>> CreateDepartment(DepartmentCreateDto departmentDto)
        {
            if (!await _facultyRepository.Exists(departmentDto.FacultyId))
            {
                return NotFound();
            }

            var department = _mapper.Map<Entities.Department>(departmentDto);

            await _facultyRepository.AddDepartmentToFaculty(departmentDto.FacultyId, department);

            await _facultyRepository.SaveChanges();

            var departmentToReturn = _mapper.Map<DepartmentWithoutFieldOfStudyDto>(department);

            return Ok(departmentToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(Guid facultyId, InstitutionUpdateDto departmentDto)
        {
            if (!await _facultyRepository.Exists(facultyId))
            {
                return NotFound("Faculty not found");
            }

            var oldDepartment = await _departmentRepository.GetById(departmentDto.Id);
            if (oldDepartment == null || oldDepartment.FacultyId != facultyId) { return NotFound("Department not found for the faculty"); }

            _mapper.Map(departmentDto, oldDepartment);
            await _departmentRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(Guid facultyId, Guid departmentId)
        {
            if (!await _facultyRepository.Exists(facultyId))
            {
                return NotFound("Faculty not found");
            }

            var oldDepartment = await _departmentRepository.GetById(departmentId);
            if (oldDepartment == null || oldDepartment.FacultyId != facultyId) { return NotFound("Department not found for the faculty"); }

            _departmentRepository.DeleteDepartment(oldDepartment);
            await _departmentRepository.SaveChanges();

            return Ok();
        }
    }
}
