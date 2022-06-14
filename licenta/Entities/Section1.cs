using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("InstitutionId")]
        public Institution? Institution { get; set; }
        public Guid InstitutionId { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }
        public Guid FacultyId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }

        [ForeignKey("FieldOfStudyId")]
        public FieldOfStudy? FieldOfStudy { get; set; }
        public Guid FieldOfStudyId { get; set; }

        [Required]
        public string CycleOfStudy { get; set; } = String.Empty;
        [Required]
        public string ProgramOfStudy { get; set; } = String.Empty;
        [Required]
        public string Qualification { get; set; } = String.Empty;
        [Required]
        public string FormOfEducation { get; set; } = String.Empty;

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }
    }
}
