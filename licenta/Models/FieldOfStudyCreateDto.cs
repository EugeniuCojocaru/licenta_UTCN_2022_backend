using System.ComponentModel.DataAnnotations;

namespace licenta.Models
{
    public class FieldOfStudyCreateDto
    {
        [Required(ErrorMessage = "You should provide a name.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
