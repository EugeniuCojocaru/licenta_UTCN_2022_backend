using licenta.Entities;

namespace licenta.Services.InstitutionHierarchy
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAll();
        Task<Faculty?> GetById(Guid id);
        Task<IEnumerable<Faculty>> GetAllByInstitutionId(Guid institutionId);
        Task<bool> Exists(Guid id);
        Task AddDepartmentToFaculty(Guid facultyId, Department department);
        Task<bool> SaveChanges();

        void DeleteFaculty(Faculty faculty);
    }
}
