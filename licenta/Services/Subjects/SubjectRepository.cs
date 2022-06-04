using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Subjects
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly EntityContext _context;

        public SubjectRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateSubject(Subject newSubject)
        {
            if (newSubject != null)
            {
                _context.Entry(newSubject).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteSubject(Subject subject)
        {
            _context.Subjects.Remove(subject);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Subjects.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> Exists(string code)
        {
            return await _context.Subjects.AnyAsync(i => i.Code == code);
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _context.Subjects.OrderBy(i => i.Code).ToListAsync();
        }

        public async Task<Subject?> GetById(Guid id)
        {
            return await _context.Subjects.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
