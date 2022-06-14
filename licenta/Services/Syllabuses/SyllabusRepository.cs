using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly EntityContext _context;

        public SyllabusRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSyllabus(Syllabus newEntry)
        {
            if (newEntry != null)
            {
                _context.Entry(newEntry).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSyllabus(Syllabus entry)
        {
            _context.Syllabuses.Remove(entry);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Syllabuses.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Syllabus>> GetAll()
        {
            return await _context.Syllabuses.ToListAsync();
        }

        public async Task<Syllabus?> GetById(Guid id)
        {
            return await _context.Syllabuses.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
