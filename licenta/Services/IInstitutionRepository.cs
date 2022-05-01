using licenta.Entities;

namespace licenta.Services
{
    public interface IInstitutionRepository
    {
        Task<IEnumerable<Institution>> GetAll();
        Task<Institution?> GetById(Guid id);


    }
}
