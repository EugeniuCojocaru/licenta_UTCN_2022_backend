using licenta.Entities;

namespace licenta.Services.Teachers
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll(bool active);
        Task<Teacher?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(string email);
        Task<bool> SaveChanges();
        Task<bool> CreateTeacher(Teacher newTeacher);

        void DeleteTeacher(Teacher teacher);

    }
}
