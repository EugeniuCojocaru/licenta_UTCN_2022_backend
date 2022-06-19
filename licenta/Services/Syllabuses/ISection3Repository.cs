using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection3Repository
    {
        Task<IEnumerable<Section3>> GetAll();
        Task<Section3?> GetById(Guid? id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection3(Section3 newSection3);

        void DeleteSection3(Section3 section3);
    }
}
