using licenta.Entities;

namespace licenta.Services
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAll();
        Task<Faculty?> GetById(Guid id);
        Task<IEnumerable<Faculty>> GetAllByInstitutionId(Guid institutionId);
        Task<bool> Exists(Guid id);
        void CreateFaculty(Faculty faculty);
        Task AddDepartmentToFaculty(Guid facultyId, Department department);
        Task<bool> SaveChanges();
    }
}
