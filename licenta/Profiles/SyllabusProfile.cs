using AutoMapper;
using licenta.Models.Syllabuses;

namespace licenta.Profiles
{
    public class SyllabusProfile : Profile
    {
        public SyllabusProfile()
        {
            CreateMap<Entities.Syllabus, SyllabusDto>();
            CreateMap<SyllabusCreateDto, Entities.Syllabus>().ForSourceMember(source => source.section2, opt => opt.DoNotValidate())
                                                             .ForSourceMember(source => source.section1, opt => opt.DoNotValidate());
        }
    }
}
