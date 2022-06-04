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
        public string CycleOfStudy { get; set; } = "Bachelor of Science";
        [Required]
        public string ProgramOfStudy { get; set; } = "Computer science";
        [Required]
        public string Qualification { get; set; } = "Engineer";
        [Required]
        public string FormOfEducation { get; set; } = "Full time";

    }
}
