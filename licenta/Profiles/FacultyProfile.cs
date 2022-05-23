using AutoMapper;
using licenta.Models.InstitutionHierarchy;
namespace licenta.Profiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<Entities.Faculty, FacultyDto>();
            CreateMap<Entities.Faculty, FacultyWithoutDepartmentDto>();
            CreateMap<FacultyCreateDto, Entities.Faculty>();
            CreateMap<InstitutionUpdateDto, Entities.Faculty>();
        }
    }
}
