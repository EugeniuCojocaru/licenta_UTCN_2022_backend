using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section5Repository : ISection5Repository
    {
        private readonly EntityContext _context;

        public Section5Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection5(Section5 newSection5)
        {
            if (newSection5 != null)
            {
                _context.Entry(newSection5).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection5(Section5 section5)
        {
            _context.Sections5.Remove(section5);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections5.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section5>> GetAll()
        {
            return await _context.Sections5.ToListAsync();
        }

        public async Task<Section5?> GetById(Guid? id)
        {
            if (id != null)
                return await _context.Sections5.Where(i => i.Id == id).FirstOrDefaultAsync();
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
