using AutoMapper;
using licenta.Models.InstitutionHierarchy;
namespace licenta.Profiles
{
    public class FieldOfStudyProfile : Profile
    {
        public FieldOfStudyProfile()
        {
            CreateMap<Entities.FieldOfStudy, FieldOfStudyDto>();
            CreateMap<FieldOfStudyCreateDto, Entities.FieldOfStudy>();
            CreateMap<InstitutionUpdateDto, Entities.FieldOfStudy>();
        }
    }
}
