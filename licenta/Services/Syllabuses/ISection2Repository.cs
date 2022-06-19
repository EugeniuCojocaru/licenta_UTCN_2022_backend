using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection2Repository
    {
        Task<IEnumerable<Section2>> GetAll();
        Task<Section2?> GetById(Guid id);
        Task<Section2?> GetById(Guid? id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection2(Section2 newSection2);

        void DeleteSection2(Section2 section2);
    }
}
