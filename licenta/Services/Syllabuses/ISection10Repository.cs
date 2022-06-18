using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection10Repository
    {
        Task<IEnumerable<Section10>> GetAll();
        Task<Section10?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection10(Section10 newSection10);

        void DeleteSection10(Section10 section10);
    }
}
