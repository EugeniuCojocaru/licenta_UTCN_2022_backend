namespace licenta.Models.Syllabuses
{
    public class Section10Dto
    {
        public Guid Id { get; set; }
        public string CourseCriteria { get; set; } = string.Empty;
        public string CourseMethods { get; set; } = string.Empty;

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

        public string ConditionPromotion { get; set; } = string.Empty;
    }
}
