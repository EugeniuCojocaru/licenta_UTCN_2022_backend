using AutoMapper;

namespace licenta.Profiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<Entities.Faculty, Models.FacultyDto>();
            CreateMap<Entities.Faculty, Models.FacultyWithoutDepartmentDto>();
            CreateMap<Models.FacultyCreateDto, Entities.Faculty>();
        }
    }
}
