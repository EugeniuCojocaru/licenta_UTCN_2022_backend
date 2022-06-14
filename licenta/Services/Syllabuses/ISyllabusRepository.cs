using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISyllabusRepository
    {
        Task<IEnumerable<Syllabus>> GetAll();
        Task<Syllabus?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSyllabus(Syllabus newSection1);

        void DeleteSyllabus(Syllabus section1);
    }
}
