using licenta.Entities;

namespace licenta.Services.Subjects
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAll();
        Task<Subject?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> Exists(string code);
        Task<bool> SaveChanges();
        Task<bool> CreateSubject(Subject newSubject);

        void DeleteSubject(Subject subject);

    }
}
