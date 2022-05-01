using AutoMapper;
using licenta.Models;
using licenta.Services;
using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/fieldsOfStudy")]
    public class FieldOfStudyController : Controller
    {
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly IMapper _mapper;

        public FieldOfStudyController(IFieldOfStudyRepository fieldOfStudyRepository, IMapper mapper)
        {
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldOfStudyDto>>> GetFieldsOfStudy()
        {
            var fieldsOfStudyEntities = await _fieldOfStudyRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FieldOfStudyDto>>(fieldsOfStudyEntities));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FieldOfStudyDto>> GetFieldOfStudy(Guid id)
        {
            var fieldOfStudy = await _fieldOfStudyRepository.GetById(id);
            if (fieldOfStudy == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FieldOfStudyDto>(fieldOfStudy));

        }

        //[HttpPost]
        //public async Task<ActionResult<FieldOfStudyDto>> CreateFieldOfStudy()
        //{

        //}

        //public async Task<bool> SaveChanges()
        //{
        //    return (await _conte)
        //}
    }
}
