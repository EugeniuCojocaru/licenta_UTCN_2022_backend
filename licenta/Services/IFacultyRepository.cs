using licenta.Entities;

namespace licenta.Services
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAll();

        Task<Faculty?> GetById(Guid id);

    }
}
