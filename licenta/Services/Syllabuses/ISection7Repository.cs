using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection7Repository
    {
        Task<IEnumerable<Section7>> GetAll();
        Task<Section7?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection7(Section7 newSection7);

        void DeleteSection7(Section7 section7);
    }
}
