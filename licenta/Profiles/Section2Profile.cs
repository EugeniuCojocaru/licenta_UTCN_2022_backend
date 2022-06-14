using AutoMapper;
using licenta.Models.Syllabuses;

namespace licenta.Profiles
{
    public class Section2Profile : Profile
    {
        public Section2Profile()
        {
            CreateMap<Entities.Section2, Section2Dto>();
            CreateMap<Section2CreateDto, Entities.Section2>().ForSourceMember(source => source.Teachers, opt => opt.DoNotValidate());
        }
    }
}
