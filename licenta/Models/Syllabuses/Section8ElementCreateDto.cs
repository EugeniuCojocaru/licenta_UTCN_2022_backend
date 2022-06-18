using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section8ElementCreateDto
    {
        [Required(ErrorMessage = "You should provide the name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the duration")]
        public int Duration { get; set; } = 0;
        public string Note { get; set; } = string.Empty;
    }
}
