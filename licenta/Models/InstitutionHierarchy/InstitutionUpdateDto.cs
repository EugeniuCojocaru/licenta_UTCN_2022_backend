using System.ComponentModel.DataAnnotations;

namespace licenta.Models.InstitutionHierarchy
{
    public class InstitutionUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the ID")]
        public Guid Id { get; set; }
    }
}
