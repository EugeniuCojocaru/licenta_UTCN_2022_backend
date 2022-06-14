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
        public int YearOfStudy { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public TypeOfAssessment Assessment { get; set; }
        [Required]
        public SubjectCategory1 Category1 { get; set; }
        [Required]
        public SubjectCategory2 Category2 { get; set; }

        [Required]
        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }
        public Guid TeacherId { get; set; }

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }
    }
}

