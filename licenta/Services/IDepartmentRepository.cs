using licenta.Entities;

namespace licenta.Services
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> GetById(Guid id);
        Task<bool> Exists(Guid id);
    }
}
