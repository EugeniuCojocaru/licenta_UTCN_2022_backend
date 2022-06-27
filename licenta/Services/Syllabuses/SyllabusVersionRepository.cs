using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class SyllabusVersionRepository : ISyllabusVersionRepository
    {
        private readonly EntityContext _context;

        public SyllabusVersionRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> CreateSyllabusVersion(SyllabusVersion newEntry)
        {
            if (newEntry != null)
            {
                _context.Entry(newEntry).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public async Task<bool> Exists(Guid SyllabusVersionId)
        {
            return await _context.SyllabusVersions.AnyAsync(i => i.Id == SyllabusVersionId);
        }

        public async Task<IEnumerable<SyllabusVersion>> GetAllBySubjectId(Guid SubjectId)
        {
            return await _context.SyllabusVersions.Where(i => i.Syllabus.SubjectId == SubjectId).ToListAsync();

        }

        public async Task<SyllabusVersion?> GetById(Guid SyllabusVersionId)
        {
            return await _context.SyllabusVersions.Where(i => i.Id == SyllabusVersionId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> SubjectHasSyllabus(Guid SubjectId)
        {
            return await _context.SyllabusVersions.AnyAsync(i => i.Syllabus.SubjectId == SubjectId && i.UpdatedAt == DateTime.MinValue);
        }
    }
}
