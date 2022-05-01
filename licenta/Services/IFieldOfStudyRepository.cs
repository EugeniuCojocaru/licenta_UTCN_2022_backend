using licenta.Entities;

namespace licenta.Services
{
    public interface IFieldOfStudyRepository
    {
        Task<IEnumerable<FieldOfStudy>> GetAll();
        Task<FieldOfStudy?> GetById(Guid id);
        Task<bool> SaveChanges();
    }
}
