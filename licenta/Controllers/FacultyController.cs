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
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyRepository facultyRepository, IInstitutionRepository institutionRepository, IMapper mapper)
        {
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyWithoutDepartmentDto>>> GetFaculties()
        {
            var facultyEntities = await _facultyRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FacultyWithoutDepartmentDto>>(facultyEntities));

        }

        [HttpGet("{facultyId}", Name = "GetFaculty")]
        public async Task<ActionResult> GetFaculty(Guid facultyId, bool includeDepartments = false)
        {
            var faculty = await _facultyRepository.GetById(facultyId);
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



        [HttpPost]
        public async Task<ActionResult<FacultyWithoutDepartmentDto>> CreateFaculty(Guid institutionId, FacultyCreateDto facultyDto)
        {
            if (!await _institutionRepository.Exists(institutionId))
            {
                return NotFound();
            }

            var faculty = _mapper.Map<Entities.Faculty>(facultyDto);

            await _institutionRepository.AddFacultyToInstitution(institutionId, faculty);

            await _institutionRepository.SaveChanges();

            var facultyToReturn = _mapper.Map<FacultyWithoutDepartmentDto>(faculty);

            return Ok(facultyToReturn);
        }
    }
}
