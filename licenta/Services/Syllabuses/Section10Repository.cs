using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section10Repository : ISection10Repository
    {
        private readonly EntityContext _context;

        public Section10Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection10(Section10 newSection10)
        {
            if (newSection10 != null)
            {
                _context.Entry(newSection10).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection10(Section10 section10)
        {
            _context.Sections10.Remove(section10);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections10.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section10>> GetAll()
        {
            return await _context.Sections10.ToListAsync();
        }

        public async Task<Section10?> GetById(Guid? id)
        {
            if (id != null)
                return await _context.Sections10.Where(i => i.Id == id).FirstOrDefaultAsync();
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
