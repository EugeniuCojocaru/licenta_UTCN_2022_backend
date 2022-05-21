using System.ComponentModel.DataAnnotations;

namespace licenta.Models
{
    public class InstitutionUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the institution's ID")]
        public Guid Id { get; set; }
    }
}
