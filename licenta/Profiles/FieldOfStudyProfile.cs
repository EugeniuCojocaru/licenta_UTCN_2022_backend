using AutoMapper;

namespace licenta.Profiles
{
    public class FieldOfStudyProfile : Profile
    {
        public FieldOfStudyProfile()
        {
            CreateMap<Entities.FieldOfStudy, Models.FieldOfStudyDto>();
            CreateMap<Models.FieldOfStudyCreateDto, Entities.FieldOfStudy>();
            CreateMap<Models.InstitutionUpdateDto, Entities.FieldOfStudy>();
        }
    }
}
