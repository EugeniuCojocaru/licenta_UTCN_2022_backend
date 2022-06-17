using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection9Repository
    {
        Task<IEnumerable<Section9>> GetAll();
        Task<Section9?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection9(Section9 newSection9);

        void DeleteSection9(Section9 section9);
    }
}
