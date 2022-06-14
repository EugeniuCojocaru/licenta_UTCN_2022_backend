using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISyllabusTeacherRepository
    {
        Task<IEnumerable<SyllabusTeacher>> GetAllBySyllabusId(Guid SyllabusId);
        Task<IEnumerable<SyllabusTeacher>> GetAllByTeacherId(Guid TeacherId);
        Task<bool> Exists(Guid SyllabusId, Guid TeacherId);
        Task<bool> SaveChanges();
        Task<bool> CreateSyllabusTeacher(SyllabusTeacher newEntry);

        void DeleteSyllabusTeacher(SyllabusTeacher entry);
    }
}
