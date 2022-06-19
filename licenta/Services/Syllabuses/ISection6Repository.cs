using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection6Repository
    {
        Task<IEnumerable<Section6>> GetAll();
        Task<Section6?> GetById(Guid? id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection6(Section6 newSection6);

        void DeleteSection6(Section6 section6);
    }
}
