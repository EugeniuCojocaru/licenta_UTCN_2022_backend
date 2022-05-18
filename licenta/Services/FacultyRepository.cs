using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly EntityContext _context;

        public FacultyRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public async Task<IEnumerable<Faculty>> GetAll()
        {
            return await _context.Faculties.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Faculty?> GetById(Guid id)
        {
            return await _context.Faculties.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Faculty>> GetAllByInstitutionId(Guid institutionId)
        {
            return await _context.Faculties.Where(i => i.Institution.Id == institutionId).ToListAsync();
        }
        public void CreateFaculty(Faculty faculty)
        {
            if (faculty != null)
            {
                _context.Faculties.Add(faculty);
            }
        }
        public async Task<bool> Exists(Guid id)
        {
            return await _context.Faculties.AnyAsync(i => i.Id == id);
        }
    }
}
