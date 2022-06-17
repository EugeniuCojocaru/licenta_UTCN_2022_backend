using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section7Repository : ISection7Repository
    {
        private readonly EntityContext _context;

        public Section7Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection7(Section7 newSection7)
        {
            if (newSection7 != null)
            {
                _context.Entry(newSection7).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection7(Section7 section7)
        {
            _context.Sections7.Remove(section7);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections7.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section7>> GetAll()
        {
            return await _context.Sections7.ToListAsync();
        }

        public async Task<Section7?> GetById(Guid id)
        {
            return await _context.Sections7.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
