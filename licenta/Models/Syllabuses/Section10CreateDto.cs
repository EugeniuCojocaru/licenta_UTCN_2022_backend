using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section10CreateDto
    {
        [Required(ErrorMessage = "You should provide the course assessment criteria")]
        public string CourseCriteria { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the course assessment methods")]
        public string CourseMethods { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the course wight for the final grade")]
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
        public List<string> ConditionsFinalExam { get; set; } = new List<string>();
        [Required(ErrorMessage = "You should provide the promotion conditions")]
        public string ConditionPromotion { get; set; } = string.Empty;
    }
}
