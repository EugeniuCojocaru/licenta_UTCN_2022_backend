using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;
using static licenta.Entities.Constants;

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
        public async Task<Guid?> Exists(string email, string password)
        {

            var user = await _context.Teachers.Where(i => i.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return user.Id;
            }

            return null;
        }

        public async Task<IEnumerable<Teacher>> GetAll(bool active)
        {
            if (active)
                return await _context.Teachers.Where(i => i.Active == true).OrderBy(i => i.Name).ToListAsync();

            return await _context.Teachers.OrderBy(i => i.Name).ToListAsync();

        }

        public async Task<Teacher?> GetById(Guid id)
        {
            return await _context.Teachers.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Role?> GetRoleById(Guid? id)
        {
            if (id != null)
            {
                var user = await _context.Teachers.Where(i => i.Id == id).FirstOrDefaultAsync();
                return user.Role;
            }
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
