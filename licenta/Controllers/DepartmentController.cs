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
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
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


        [HttpPost]
        public async Task<ActionResult<DepartmentWithoutFieldOfStudyDto>> CreateFaculty(DepartmentCreateDto departmentDto)
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
