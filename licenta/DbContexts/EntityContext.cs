using licenta.Entities;
using Microsoft.EntityFrameworkCore;

namespace licenta.DbContexts
{
    public class EntityContext : DbContext
    {
        public DbSet<Institution> Institutions { get; set; } = null!;
        public DbSet<Faculty> Faculties { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<FieldOfStudy> FieldsOfStudy { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=licenta;Username=postgres;Password=admin");
    }
}

