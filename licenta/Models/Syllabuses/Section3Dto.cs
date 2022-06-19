namespace licenta.Models.Syllabuses
{
    public class Section3Dto
    {
        public Guid Id { get; set; }
        public int CourseHoursPerWeek { get; set; }
        public int SeminarHoursPerWeek { get; set; }

        public int LaboratoryHoursPerWeek { get; set; }

        public int ProjectHoursPerWeek { get; set; }

        public int CourseHoursPerSemester { get; set; }

        public int SeminarHoursPerSemester { get; set; }

        public int LaboratoryHoursPerSemester { get; set; }

        public int ProjectHoursPerSemester { get; set; }

        public int IndividualStudyA { get; set; }
        public int IndividualStudyB { get; set; }
        public int IndividualStudyC { get; set; }
        public int IndividualStudyD { get; set; }
        public int IndividualStudyE { get; set; }
        public int IndividualStudyF { get; set; }

        public int Credits { get; set; }
    }
}
