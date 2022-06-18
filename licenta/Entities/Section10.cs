using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section10
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string CourseCriteria { get; set; } = string.Empty;
        [Required]
        public string CourseMethods { get; set; } = string.Empty;
        [Required]
        public int CourcePercentage { get; set; } = 0;
        public string SeminarCriteria { get; set; } = string.Empty;
        public string SeminarMethods { get; set; } = string.Empty;
        public int SeminarPercentage { get; set; } = 0;
        public string LaboratoryCriteria { get; set; } = string.Empty;
        public string LaboratoryMethods { get; set; } = string.Empty;
        public int LaboratoryPercentage { get; set; } = 0;
        public string ProjectCriteria { get; set; } = string.Empty;
        public string ProjectMethods { get; set; } = string.Empty;
        public int ProjectPercentage { get; set; } = 0;
        public string MinimumPerformance { get; set; } = string.Empty;
        public string ConditionsFinalExam { get; set; } = string.Empty;
        [Required]
        public string ConditionsPromotion { get; set; } = string.Empty;

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }

    }
}
