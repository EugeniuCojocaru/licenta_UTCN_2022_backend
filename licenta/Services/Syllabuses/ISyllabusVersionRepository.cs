using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISyllabusVersionRepository
    {
        Task<IEnumerable<SyllabusVersion>> GetAllBySubjectId(Guid SubjectId);
        Task<SyllabusVersion?> GetById(Guid SubjectId);
        Task<bool> Exists(Guid SyllabusVersionId);
        Task<bool> SubjectHasSyllabus(Guid SubjectId);
        Task<bool> SaveChanges();
        Task<bool> CreateSyllabusVersion(SyllabusVersion newEntry);
    }
}
