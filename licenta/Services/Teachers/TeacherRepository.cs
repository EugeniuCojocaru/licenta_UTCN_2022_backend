using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Teachers
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly EntityContext _context;

        public TeacherRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateTeacher(Teacher newTeacher)
        {
            if (newTeacher != null)
            {
                _context.Entry(newTeacher).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public void DeleteTeacher(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Teachers.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.Teachers.AnyAsync(i => i.Email == email);
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await _context.Teachers.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Teacher?> GetById(Guid id)
        {
            return await _context.Teachers.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
