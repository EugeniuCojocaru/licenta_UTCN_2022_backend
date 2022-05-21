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
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public FieldOfStudyController(IFieldOfStudyRepository fieldOfStudyRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
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

        [HttpPost]
        public async Task<ActionResult<FieldOfStudyDto>> CreateFieldOfStudy(FieldOfStudyCreateDto fieldOfStudyDto)
        {
            if (!await _departmentRepository.Exists(fieldOfStudyDto.DepartmentId))
            {
                return NotFound();
            }

            var fieldOfStudy = _mapper.Map<Entities.FieldOfStudy>(fieldOfStudyDto);

            await _departmentRepository.AddFieldOfStudyToDepartment(fieldOfStudyDto.DepartmentId, fieldOfStudy);

            await _departmentRepository.SaveChanges();

            var fieldOfStudyToReturn = _mapper.Map<FieldOfStudyDto>(fieldOfStudy);

            return Ok(fieldOfStudyToReturn);
        }
    }
}
