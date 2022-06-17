using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class Section4SubjectRepository : ISection4SubjectRepository
    {
        private readonly EntityContext _context;

        public Section4SubjectRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSection4Teacher(Section4Subject newEntry)
        {
            if (newEntry != null)
            {
                _context.Entry(newEntry).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSection4Teacher(Section4Subject entry)
        {
            _context.Section4Subjects.Remove(entry);
        }

        public async Task<bool> Exists(Guid Section4Id, Guid SubjectId)
        {
            return await _context.Section4Subjects.AnyAsync(i => i.Section4Id == Section4Id && i.SubjectId == SubjectId);
        }

        public async Task<IEnumerable<Section4Subject>> GetAllBySection4Id(Guid Section4Id)
        {
            return await _context.Section4Subjects.Where(i => i.Section4Id == Section4Id).ToListAsync();
        }

        public async Task<IEnumerable<Section4Subject>> GetAllBySubjectId(Guid SubjectId)
        {
            return await _context.Section4Subjects.Where(i => i.SubjectId == SubjectId).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
