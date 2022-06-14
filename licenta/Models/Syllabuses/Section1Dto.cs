namespace licenta.Models.Syllabuses
{
    public class Section1Dto
    {
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public Guid FacultyId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid FieldOfStudyId { get; set; }

        public string CycleOfStudy { get; set; } = string.Empty;
        public string ProgramOfStudy { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string FormOfEducation { get; set; } = string.Empty;

    }
}
