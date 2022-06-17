using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section3CreateDto
    {
        [Required(ErrorMessage = "You should provide the course hours per week")]
        public int CourseHoursPerWeek { get; set; }
        public int SeminarHoursPerWeek { get; set; }

        public int LaboratoryHoursPerWeek { get; set; }

        public int ProjectHoursPerWeek { get; set; }

        [Required(ErrorMessage = "You should provide the course hours per semester")]
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


        [Required(ErrorMessage = "You should provide the credits for the course")]
        public int Credits { get; set; }
    }
}
