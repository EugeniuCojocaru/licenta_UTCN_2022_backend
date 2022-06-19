using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection8ElementRepository
    {
        Task<IEnumerable<Section8Element>> GetAll();
        Task<IEnumerable<Section8Element>> GetAllBySection8Id(Guid id);
        Task<Section8Element?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection8Element(Section8Element newSection8Element);

        void DeleteSection8Element(Section8Element section8Element);
    }
}
