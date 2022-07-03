using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services.Audits
{
    public class AuditRepository : IAuditRepository
    {
        private readonly EntityContext _context;

        public AuditRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateAudit(Audit audit)
        {
            if (audit != null)
            {
                _context.Entry(audit).State = EntityState.Added;
                return (await _context.SaveChangesAsync() == 1);
            }
            return false;
        }

        public async Task<IEnumerable<Audit>> GetAll()
        {
            return await _context.Audits.OrderByDescending(i => i.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Audit>> GetByMonth()
        {
            return await _context.Audits.Where(i => i.CreatedAt.Month.Equals(DateTime.Now.Month)).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
