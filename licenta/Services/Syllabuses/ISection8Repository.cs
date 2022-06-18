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
        Task AddElementToSection8(Guid section8Id, Section8Element section8Element);
        void DeleteSection8(Section8 section8);
    }
}
