using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section8Repository : ISection8Repository
    {
        private readonly EntityContext _context;

        public Section8Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection8(Section8 newSection8)
        {
            if (newSection8 != null)
            {
                _context.Entry(newSection8).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection8(Section8 section8)
        {
            _context.Sections8.Remove(section8);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections8.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section8>> GetAll()
        {
            return await _context.Sections8.ToListAsync();
        }

        public async Task<Section8?> GetById(Guid id)
        {
            return await _context.Sections8.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
