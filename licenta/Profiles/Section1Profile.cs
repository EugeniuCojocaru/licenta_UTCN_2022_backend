using AutoMapper;
using licenta.Models.Syllabuses;

namespace licenta.Profiles
{
    public class Section1Profile : Profile
    {
        public Section1Profile()
        {
            CreateMap<Entities.Section1, Section1Dto>();
            CreateMap<Section1CreateDto, Entities.Section1>();

        }
    }
}
