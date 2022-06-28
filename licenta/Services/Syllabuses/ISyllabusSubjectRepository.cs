using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISyllabusSubjectRepository
    {
        Task<IEnumerable<SyllabusSubject>> GetAllBySyllabusId(Guid SyllabusId);
        Task<IEnumerable<SyllabusSubject>> GetAllBySubjectId(Guid SubjectId);
        Task<bool> Exists(Guid SyllabusId, Guid SubjectId);
        Task<bool> SaveChanges();
        Task<bool> CreateSyllabusSubject(SyllabusSubject newEntry);
        void DeleteAllBySyllabusId(Guid syllabusId);
        void DeleteSyllabusSubject(SyllabusSubject entry);
    }
}
