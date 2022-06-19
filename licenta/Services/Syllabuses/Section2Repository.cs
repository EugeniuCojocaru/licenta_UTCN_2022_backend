using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section2Repository : ISection2Repository
    {
        private readonly EntityContext _context;

        public Section2Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection2(Section2 newSection2)
        {

            if (newSection2 != null)
            {
                _context.Entry(newSection2).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection2(Section2 section2)
        {
            _context.Sections2.Remove(section2);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections2.AnyAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Section2>> GetAll()
        {
            return await _context.Sections2.ToListAsync();
        }

        public async Task<Section2?> GetById(Guid id)
        {
            return await _context.Sections2.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Section2?> GetById(Guid? id)
        {
            if (id != null)
                return await _context.Sections2.Where(i => i.Id == id).FirstOrDefaultAsync();
            return null;
        }
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
