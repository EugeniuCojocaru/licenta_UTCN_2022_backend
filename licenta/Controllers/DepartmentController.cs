using AutoMapper;
using licenta.Models;
using licenta.Services;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/department")]
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

            var facultyToReturn = _mapper.Map<DepartmentWithoutFieldOfStudyDto>(department);

            return Ok(facultyToReturn);
        }
    }
}
