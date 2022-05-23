using licenta.Entities;

namespace licenta.Services.InstitutionHierarchy
{
    public interface IFieldOfStudyRepository
    {
        Task<IEnumerable<FieldOfStudy>> GetAll();
        Task<FieldOfStudy?> GetById(Guid id);
        Task<IEnumerable<FieldOfStudy>> GetAllByDepartmentId(Guid departmentId);
        void DeleteFieldOfStudy(FieldOfStudy fieldOfStudy);
        Task<bool> SaveChanges();
    }
}
