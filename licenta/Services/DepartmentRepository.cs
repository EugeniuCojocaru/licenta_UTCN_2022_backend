using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EntityContext _context;

        public DepartmentRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Department?> GetById(Guid id)
        {
            return await _context.Departments.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> Exists(Guid id)
        {
            return await _context.Departments.AnyAsync();
        }
        public async Task<IEnumerable<Department>> GetAllByFacultyId(Guid facultyId)
        {
            return await _context.Departments.Where(i => i.Faculty.Id == facultyId).ToListAsync();
        }
    }
}
