using AutoMapper;

namespace licenta.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<Entities.Institution, Models.InstitutionWithoutFacultyDto>();
            CreateMap<Entities.Institution, Models.InstitutionDto>();
            CreateMap<Models.InstitutionCreateDto, Entities.Institution>();
        }
    }
}
