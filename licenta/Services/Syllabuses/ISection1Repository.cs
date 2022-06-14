using licenta.Entities;

namespace licenta.Services.Syllabuses
{
    public interface ISection1Repository
    {
        Task<IEnumerable<Section1>> GetAll();
        Task<Section1?> GetById(Guid id);
        Task<bool> Exists(Guid id);
        Task<bool> SaveChanges();
        Task<bool> CreateSection1(Section1 newSection1);

        void DeleteSection1(Section1 section1);

    }
}
