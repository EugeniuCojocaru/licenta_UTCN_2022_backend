using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section8ElementRepository : ISection8ElementRepository
    {
        private readonly EntityContext _context;

        public Section8ElementRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection8Element(Section8Element newSection8Element)
        {
            if (newSection8Element != null)
            {
                _context.Entry(newSection8Element).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection8Element(Section8Element section8Element)
        {
            _context.Section8Elements.Remove(section8Element);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Section8Elements.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section8Element>> GetAll()
        {
            return await _context.Section8Elements.ToListAsync();
        }

        public async Task<Section8Element?> GetById(Guid id)
        {
            return await _context.Section8Elements.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
