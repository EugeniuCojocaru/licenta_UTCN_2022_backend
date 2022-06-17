using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section3Repository : ISection3Repository
    {
        private readonly EntityContext _context;

        public Section3Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection3(Section3 newSection3)
        {
            if (newSection3 != null)
            {
                _context.Entry(newSection3).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection3(Section3 section3)
        {
            _context.Sections3.Remove(section3);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections3.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section3>> GetAll()
        {
            return await _context.Sections3.ToListAsync();
        }

        public async Task<Section3?> GetById(Guid id)
        {
            return await _context.Sections3.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
