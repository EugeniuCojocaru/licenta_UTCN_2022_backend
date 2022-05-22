using licenta.Entities;

namespace licenta.Services
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<IEnumerable<Department>> GetAllByFacultyId(Guid facultyId);
        Task AddFieldOfStudyToDepartment(Guid departmentId, FieldOfStudy fieldOfStudy);
        void DeleteDepartment(Department department);
        Task<bool> SaveChanges();
    }
}
