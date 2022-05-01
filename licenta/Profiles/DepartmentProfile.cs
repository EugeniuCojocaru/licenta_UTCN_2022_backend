using AutoMapper;

namespace licenta.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Entities.Department, Models.DepartmentDto>();
            CreateMap<Entities.Department, Models.DepartmentWithoutFieldOfStudyDto>();
            CreateMap<Models.DepartmentCreateDto, Entities.Department>();
        }
    }
}
