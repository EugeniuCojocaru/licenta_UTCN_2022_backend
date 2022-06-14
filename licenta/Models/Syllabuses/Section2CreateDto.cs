using System.ComponentModel.DataAnnotations;
using static licenta.Entities.Constants;

namespace licenta.Models.Syllabuses
{
    public class Section2CreateDto
    {
        [Required(ErrorMessage = "You should provide the year of study for the section2")]
        public int YearOfStudy { get; set; }

        [Required(ErrorMessage = "You should provide the semester for the section2")]
        public int Semester { get; set; }

        [Required(ErrorMessage = "You should provide the type of assessment for the section2")]
        public TypeOfAssessment Assessment { get; set; }

        [Required(ErrorMessage = "You should provide the subject category 1 for the section2")]
        public SubjectCategory1 Category1 { get; set; }

        [Required(ErrorMessage = "You should provide the subject category 2 for the section2")]
        public SubjectCategory2 Category2 { get; set; }

        [Required(ErrorMessage = "You should provide the lecturer for the section2")]
        public Guid TeacherId { get; set; }

        public List<Guid>? Teachers { get; set; } = new List<Guid>();
    }
}
