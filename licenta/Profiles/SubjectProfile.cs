using AutoMapper;
using licenta.Models.Subjects;
namespace licenta.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Entities.Subject, SubjectDto>();
            CreateMap<SubjectCreateDto, Entities.Subject>();
            CreateMap<SubjectUpdateDto, Entities.Subject>();
        }

    }
}
