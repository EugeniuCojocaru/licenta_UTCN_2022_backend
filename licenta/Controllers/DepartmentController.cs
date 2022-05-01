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
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentWithoutFieldOfStudyDto>>> GetDepartments()
        {
            var departmentEntities = await _departmentRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<DepartmentWithoutFieldOfStudyDto>>(departmentEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(Guid id, bool includeFiledsOfStudy = false)
        {
            var department = await _departmentRepository.GetById(id);
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
    }
}
