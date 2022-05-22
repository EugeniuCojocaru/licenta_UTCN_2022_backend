using licenta.DbContexts;
using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.Services
{
    public class FieldOfStudyRepository : IFieldOfStudyRepository
    {
        private readonly EntityContext _context;

        public FieldOfStudyRepository(EntityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FieldOfStudy>> GetAll()
        {
            return await _context.FieldsOfStudy.OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<FieldOfStudy?> GetById(Guid id)
        {
            return await _context.FieldsOfStudy.Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<FieldOfStudy>> GetAllByDepartmentId(Guid departmentId)
        {
            return await _context.FieldsOfStudy.Where(i => i.Department.Id == departmentId).ToListAsync();
        }
        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteFieldOfStudy(FieldOfStudy fieldOfStudy)
        {
            _context.FieldsOfStudy.Remove(fieldOfStudy);
        }
    }
}
