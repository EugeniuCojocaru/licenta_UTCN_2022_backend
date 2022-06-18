using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class SyllabusSubjectRepository : ISyllabusSubjectRepository
    {
        private readonly EntityContext _context;

        public SyllabusSubjectRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSyllabusSubject(SyllabusSubject newEntry)
        {
            if (newEntry != null)
            {
                _context.Entry(newEntry).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSyllabusSubject(SyllabusSubject entry)
        {
            _context.SyllabusSubjects.Remove(entry);
        }

        public async Task<bool> Exists(Guid SyllabusId, Guid SubjectId)
        {
            return await _context.SyllabusSubjects.AnyAsync(i => i.SyllabusId == SyllabusId && i.SubjectId == SubjectId);
        }

        public async Task<IEnumerable<SyllabusSubject>> GetAllBySyllabusId(Guid SyllabusId)
        {
            return await _context.SyllabusSubjects.Where(i => i.SyllabusId == SyllabusId).ToListAsync();
        }

        public async Task<IEnumerable<SyllabusSubject>> GetAllBySubjectId(Guid SubjectId)
        {
            return await _context.SyllabusSubjects.Where(i => i.SubjectId == SubjectId).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
