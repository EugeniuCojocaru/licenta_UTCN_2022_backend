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
        [Required(ErrorMessage = "You should provide section3 data")]
        public Section3CreateDto section3 { get; set; }
        [Required(ErrorMessage = "You should provide section4 data")]
        public Section4CreateDto section4 { get; set; }
        [Required(ErrorMessage = "You should provide section5 data")]
        public Section5CreateDto section5 { get; set; }
        [Required(ErrorMessage = "You should provide section6 data")]
        public Section6CreateDto section6 { get; set; }
        [Required(ErrorMessage = "You should provide section7 data")]
        public Section7CreateDto section7 { get; set; }
        [Required(ErrorMessage = "You should provide section8 data")]
        public Section8CreateDto section8 { get; set; }
        [Required(ErrorMessage = "You should provide section9 data")]
        public Section9CreateDto section9 { get; set; }
        [Required(ErrorMessage = "You should provide section10 data")]
        public Section10CreateDto section10 { get; set; }

    }
}
