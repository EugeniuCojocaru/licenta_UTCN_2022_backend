using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class SyllabusCreateDto
    {
        [Required(ErrorMessage = "You should provide the subject's id")]
        public Guid SubjectId { get; set; }
        [Required(ErrorMessage = "You should provide section1 data")]
        public Section1CreateDto section1 { get; set; }
        [Required(ErrorMessage = "You should provide section2 data")]
        public Section2CreateDto section2 { get; set; }

    }
}
