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
    }
}
