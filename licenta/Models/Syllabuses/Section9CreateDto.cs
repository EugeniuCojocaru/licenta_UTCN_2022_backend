using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section9CreateDto
    {
        [Required(ErrorMessage = "You should provide a description")]
        public string Description { get; set; } = string.Empty;
    }
}
