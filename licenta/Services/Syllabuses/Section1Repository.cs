using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section1Repository : ISection1Repository
    {
        private readonly EntityContext _context;

        public Section1Repository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection1(Section1 newSection1)
        {
            if (newSection1 != null)
            {
                _context.Entry(newSection1).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection1(Section1 section1)
        {
            _context.Sections1.Remove(section1);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Sections1.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Section1>> GetAll()
        {
            return await _context.Sections1.ToListAsync();
        }

        public async Task<Section1?> GetById(Guid id)
        {
            return await _context.Sections1.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Section1?> GetById(Guid? id)
        {
            if (id != null)
                return await _context.Sections1.Where(i => i.Id == id).FirstOrDefaultAsync();
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
