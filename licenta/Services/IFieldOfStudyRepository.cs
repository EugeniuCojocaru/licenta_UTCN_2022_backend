using licenta.Entities;

namespace licenta.Services
{
    public interface IFieldOfStudyRepository
    {
        Task<IEnumerable<FieldOfStudy>> GetAll();
        Task<FieldOfStudy?> GetById(Guid id);
        Task<IEnumerable<FieldOfStudy>> GetAllByDepartmentId(Guid departmentId);
        Task<bool> SaveChanges();
    }
}
