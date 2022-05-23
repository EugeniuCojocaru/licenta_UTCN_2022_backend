using AutoMapper;
using licenta.Models.InstitutionHierarchy;
namespace licenta.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<Entities.Institution, InstitutionWithoutFacultyDto>();
            CreateMap<Entities.Institution, InstitutionDto>();
            CreateMap<InstitutionCreateDto, Entities.Institution>();
            CreateMap<InstitutionUpdateDto, Entities.Institution>();
        }
    }
}
