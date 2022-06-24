using licenta.Entities;
using static licenta.Entities.Constants;

namespace licenta.Services.Teachers
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll(bool active);
        Task<Teacher?> GetById(Guid id);
        Task<Role?> GetRoleById(Guid? id);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(string email);
        Task<Guid?> Exists(string email, string password);
        Task<bool> SaveChanges();
        Task<bool> CreateTeacher(Teacher newTeacher);

        void DeleteTeacher(Teacher teacher);

    }
}
