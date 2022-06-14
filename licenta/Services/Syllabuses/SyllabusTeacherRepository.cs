using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Syllabuses
{
    public class SyllabusTeacherRepository : ISyllabusTeacherRepository
    {
        private readonly EntityContext _context;

        public SyllabusTeacherRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSyllabusTeacher(SyllabusTeacher newEntry)
        {
            if (newEntry != null)
            {
                _context.Entry(newEntry).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSyllabusTeacher(SyllabusTeacher entry)
        {
            _context.SyllabusTeachers.Remove(entry);
        }

        public async Task<bool> Exists(Guid SyllabusId, Guid TeacherId)
        {
            return await _context.SyllabusTeachers.AnyAsync(i => i.SyllabusId == SyllabusId && i.TeacherId == TeacherId);
        }

        public async Task<IEnumerable<SyllabusTeacher>> GetAllBySyllabusId(Guid SyllabusId)
        {
            return await _context.SyllabusTeachers.Where(i => i.SyllabusId == SyllabusId).ToListAsync();
        }

        public async Task<IEnumerable<SyllabusTeacher>> GetAllByTeacherId(Guid TeacherId)
        {
            return await _context.SyllabusTeachers.Where(i => i.TeacherId == TeacherId).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
