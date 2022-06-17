using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection4SubjectRepository
    {
        Task<IEnumerable<Section4Subject>> GetAllBySection4Id(Guid Section4Id);
        Task<IEnumerable<Section4Subject>> GetAllBySubjectId(Guid SubjectId);
        Task<bool> Exists(Guid Section4Id, Guid SubjectId);
        Task<bool> SaveChanges();
        Task<bool> CreateSection4Teacher(Section4Subject newEntry);

        void DeleteSection4Teacher(Section4Subject entry);
    }
}
