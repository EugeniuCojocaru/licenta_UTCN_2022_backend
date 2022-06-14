using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section3
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int CourseHoursPerWeek { get; set; }

        public int SeminarHoursPerWeek { get; set; }

        public int LaboratoryHoursPerWeek { get; set; }

        public int ProjectHoursPerWeek { get; set; }
        public int HoursPerWeek
        {
            get
            {
                return CourseHoursPerWeek + SeminarHoursPerWeek + LaboratoryHoursPerWeek + ProjectHoursPerWeek;
            }
        }

        public int CourseHoursPerSemester { get; set; }

        public int SeminarHoursPerSemester { get; set; }

        public int LaboratoryHoursPerSemester { get; set; }

        public int ProjectHoursPerSemester { get; set; }
        public int HoursPerSemester
        {
            get
            {
                return CourseHoursPerSemester + SeminarHoursPerSemester + LaboratoryHoursPerSemester + ProjectHoursPerSemester;
            }
        }

        public int IndividualStudyA { get; set; }
        public int IndividualStudyB { get; set; }
        public int IndividualStudyC { get; set; }
        public int IndividualStudyD { get; set; }
        public int IndividualStudyE { get; set; }
        public int IndividualStudyF { get; set; }

        public int HoursSelfStudy
        {
            get
            {
                return IndividualStudyA + IndividualStudyB + IndividualStudyC + IndividualStudyD + IndividualStudyE + IndividualStudyF;
            }
        }
        public int TotalHoursPerSemester
        {
            get
            {
                return HoursPerSemester + HoursSelfStudy;
            }
        }
        [Required]
        public int Credits { get; set; }

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }

    }
}
