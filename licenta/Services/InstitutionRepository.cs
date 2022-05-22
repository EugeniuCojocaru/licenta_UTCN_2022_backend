using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly EntityContext _context;

        public InstitutionRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Institution>> GetAll()
        {
            return await _context.Institutions.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<Institution?> GetById(Guid id)
        {
            return await _context.Institutions.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Institutions.AnyAsync(i => i.Id == id);
        }
        public async Task<bool> Exists(string name)
        {
            return await _context.Institutions.AnyAsync(i => i.Name == name);
        }
        public async Task AddFacultyToInstitution(Guid institutionId, Faculty faculty)
        {
            var institution = await GetById(institutionId);
            if (institution != null)
            {
                institution.Faculties.Add(faculty);
            }
        }

        public async Task<bool> CreateInstitution(Institution institution)
        {
            if (institution != null)
            {
                _context.Entry(institution).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteInstitution(Institution institution)
        {
            _context.Institutions.Remove(institution);
        }
    }
}
