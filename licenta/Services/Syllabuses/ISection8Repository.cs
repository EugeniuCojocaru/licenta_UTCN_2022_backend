using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection8Repository
    {
        Task<IEnumerable<Section8>> GetAll();
        Task<Section8?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection8(Section8 newSection8);
        Task AddElementToSection8(Guid elementId, Section8 section8);
        void DeleteSection8(Section8 section8);
    }
}
