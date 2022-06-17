using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section9Repository : ISection9Repository
    {
        private readonly EntityContext _context;

        public Section9Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection9(Section9 newSection9)
        {
            if (newSection9 != null)
            {
                _context.Entry(newSection9).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection9(Section9 section9)
        {
            _context.Sections9.Remove(section9);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections9.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section9>> GetAll()
        {
            return await _context.Sections9.ToListAsync();
        }

        public async Task<Section9?> GetById(Guid id)
        {
            return await _context.Sections9.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
