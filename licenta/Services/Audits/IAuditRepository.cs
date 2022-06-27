using licenta.Entities;

namespace licenta.Services.Audits
{
    public interface IAuditRepository
    {
        Task<IEnumerable<Audit>> GetAll();
        Task<bool> SaveChanges();
        Task<bool> CreateAudit(Audit Audit);

    }
}
