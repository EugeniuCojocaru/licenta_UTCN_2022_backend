using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection4Repository
    {
        Task<IEnumerable<Section4>> GetAll();
        Task<Section4?> GetById(Guid? id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection4(Section4 newSection4);

        void DeleteSection4(Section4 section4);
    }
}
