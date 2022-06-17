using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection5Repository
    {
        Task<IEnumerable<Section5>> GetAll();
        Task<Section5?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection5(Section5 newSection5);

        void DeleteSection5(Section5 section5);
    }
}
