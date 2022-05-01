using AutoMapper;
using licenta.Models;
using licenta.Services;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/faculties")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyRepository facultyRepository, IMapper mapper)
        {
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyWithoutDepartmentDto>>> GetFaculties()
        {
            var facultyEntities = await _facultyRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FacultyWithoutDepartmentDto>>(facultyEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFaculty(Guid id, bool includeDepartments = false)
        {
            var faculty = await _facultyRepository.GetById(id);
            if (faculty == null)
            {
                return NotFound();
            }
            if (includeDepartments)
            {
                return Ok(_mapper.Map<FacultyDto>(faculty));
            }
            return Ok(_mapper.Map<FacultyWithoutDepartmentDto>(faculty));

        }
    }
}
