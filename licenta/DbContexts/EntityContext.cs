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
        public DbSet<Syllabus> Syllabuses { get; set; } = null!;
        public DbSet<SyllabusTeacher> SyllabusTeachers { get; set; } = null!;
        public DbSet<Section1> Sections1 { get; set; } = null!;
        public DbSet<Section2> Sections2 { get; set; } = null!;
        public DbSet<Section3> Sections3 { get; set; } = null!;
        public DbSet<Section4> Sections4 { get; set; } = null!;
        public DbSet<Section4Subject> Section4Subjects { get; set; } = null!;
        public DbSet<Section5> Sections5 { get; set; } = null!;
        public DbSet<Section6> Sections6 { get; set; } = null!;
        public DbSet<Section7> Sections7 { get; set; } = null!;
        public DbSet<Section8> Sections8 { get; set; } = null!;
        public DbSet<Section8Element> Section8Elements { get; set; } = null!;
        public DbSet<Section9> Sections9 { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=licenta;Username=postgres;Password=admin");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SyllabusTeacher>()
                .HasKey(st => new { st.TeacherId, st.SyllabusId });
            modelBuilder.Entity<Section4Subject>()
               .HasKey(ss => new { ss.SubjectId, ss.Section4Id });
        }
    }
}

