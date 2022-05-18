﻿using licenta.DbContexts;
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

        public Task<bool> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}