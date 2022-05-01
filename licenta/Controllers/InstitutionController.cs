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
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionRepository institutionRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
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

    }
}
