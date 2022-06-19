using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section4Repository : ISection4Repository
    {
        private readonly EntityContext _context;

        public Section4Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection4(Section4 newSection4)
        {
            if (newSection4 != null)
            {
                _context.Entry(newSection4).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection4(Section4 section4)
        {
            _context.Sections4.Remove(section4);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections4.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section4>> GetAll()
        {
            return await _context.Sections4.ToListAsync();
        }

        public async Task<Section4?> GetById(Guid? id)
        {
            if (id != null)
                return await _context.Sections4.Where(i => i.Id == id).FirstOrDefaultAsync();
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
