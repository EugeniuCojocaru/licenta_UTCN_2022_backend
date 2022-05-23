using licenta.Entities;

namespace licenta.Services.InstitutionHierarchy
{
    public interface IInstitutionRepository
    {
        Task<IEnumerable<Institution>> GetAll();
        Task<Institution?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(string name);
        Task AddFacultyToInstitution(Guid institutionId, Faculty faculty);
        Task<bool> SaveChanges();
        Task<bool> CreateInstitution(Institution institution);

        void DeleteInstitution(Institution institution);

    }
}
