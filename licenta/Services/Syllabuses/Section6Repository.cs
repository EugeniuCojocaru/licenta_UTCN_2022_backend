using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section6Repository : ISection6Repository
    {
        private readonly EntityContext _context;

        public Section6Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection6(Section6 newSection6)
        {
            if (newSection6 != null)
            {
                _context.Entry(newSection6).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection6(Section6 section6)
        {
            _context.Sections6.Remove(section6);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections6.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section6>> GetAll()
        {
            return await _context.Sections6.ToListAsync();
        }

        public async Task<Section6?> GetById(Guid id)
        {
            return await _context.Sections6.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
