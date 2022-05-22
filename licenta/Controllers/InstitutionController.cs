using AutoMapper;
using licenta.Models;
using licenta.Services;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/institutions")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionRepository institutionRepository, IFacultyRepository facultyRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
            _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionWithoutFacultyDto>>> GetInstitutions()
        {
            var institutionEntities = await _institutionRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<InstitutionWithoutFacultyDto>>(institutionEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetInstitution(Guid id, bool includeFaculties = false)
        {
            var institution = await _institutionRepository.GetById(id);
            if (institution == null)
            {
                return NotFound();
            }
            if (includeFaculties)
            {
                return Ok(_mapper.Map<InstitutionDto>(institution));
            }
            return Ok(_mapper.Map<InstitutionWithoutFacultyDto>(institution));

        }

        [HttpGet("{id}/faculties")]
        public async Task<ActionResult> GetFacultyByInstitutionId(Guid id)
        {
            var institution = await _institutionRepository.GetById(id);

            if (institution == null)
            {
                return NotFound();
            }
            var facultyEntities = await _facultyRepository.GetAllByInstitutionId(id);

            return Ok(_mapper.Map<IEnumerable<FacultyWithoutDepartmentDto>>(facultyEntities));

        }

        [HttpPost]
        public async Task<ActionResult<InstitutionDto>> CreateInstitution(InstitutionCreateDto newInstitution)
        {
            if (await _institutionRepository.Exists(newInstitution.Name))
            {
                return Conflict("Same name not allowed");
            }
            var dbInstitution = _mapper.Map<Entities.Institution>(newInstitution);

            await _institutionRepository.CreateInstitution(dbInstitution);

            var institutionToReturn = _mapper.Map<InstitutionDto>(dbInstitution);
            return Ok(institutionToReturn);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateInstitution(InstitutionUpdateDto updatedInstitution)
        {
            var oldInstitution = await _institutionRepository.GetById(updatedInstitution.Id);
            if (oldInstitution == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedInstitution, oldInstitution);
            await _institutionRepository.SaveChanges();

            return Ok();
        }
    }
}
