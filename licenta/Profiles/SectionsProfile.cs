using AutoMapper;
using licenta.Models.Syllabuses;

namespace licenta.Profiles
{
    public class SectionsProfile : Profile
    {
        public SectionsProfile()
        {
            CreateMap<Entities.Section1, Section1Dto>();
            CreateMap<Section1CreateDto, Entities.Section1>();
            CreateMap<Entities.Section2, Section2Dto>();
            CreateMap<Section2CreateDto, Entities.Section2>().ForSourceMember(source => source.Teachers, opt => opt.DoNotValidate());
            //CreateMap<Entities.Section3, Section3Dto>();
            CreateMap<Section3CreateDto, Entities.Section3>();
            //CreateMap<Entities.Section3, Section3Dto>();
            CreateMap<Section4CreateDto, Entities.Section4>().ForSourceMember(source => source.Subjects, opt => opt.DoNotValidate());
            //CreateMap<Entities.Section5, Section5Dto>();
            CreateMap<Section9CreateDto, Entities.Section9>();
        }
    }
}
