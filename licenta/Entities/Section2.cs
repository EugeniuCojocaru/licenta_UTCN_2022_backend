using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static licenta.Entities.Constants;

namespace licenta.Entities
{
    public class Section2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int YearOfStudy { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public TypeOfAssessment Assessment { get; set; }
        public SubjectCategory1 Category1 { get; set; }
        public SubjectCategory2 Category2 { get; set; }


        [ForeignKey("TeacherId")]
        public Teacher? Lecturer { get; set; }
        public Guid LecturerId { get; set; }
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
