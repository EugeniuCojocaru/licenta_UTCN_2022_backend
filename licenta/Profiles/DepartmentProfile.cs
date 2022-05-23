using AutoMapper;
using licenta.Models.InstitutionHierarchy;

namespace licenta.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Entities.Department, DepartmentDto>();
            CreateMap<Entities.Department, DepartmentWithoutFieldOfStudyDto>();
            CreateMap<DepartmentCreateDto, Entities.Department>();
            CreateMap<InstitutionUpdateDto, Entities.Department>();
        }
    }
}
