using AutoMapper;
using licenta.Models.Teachers;

namespace licenta.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Entities.Teacher, TeacherDto>();
            CreateMap<Entities.Teacher, TeacherListDto>();
            CreateMap<TeacherCreateDto, Entities.Teacher>();
            CreateMap<TeacherUpdateDto, Entities.Teacher>();
        }
    }
}
