using AutoMapper;
using licenta.Models.Syllabuses;

namespace licenta.Profiles
{
    public class SyllabusProfile : Profile
    {
        public SyllabusProfile()
        {
            CreateMap<Entities.Syllabus, SyllabusDto>();
        }
    }
}
