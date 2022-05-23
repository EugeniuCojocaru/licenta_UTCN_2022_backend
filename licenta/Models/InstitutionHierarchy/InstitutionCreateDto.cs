using System.ComponentModel.DataAnnotations;

namespace licenta.Models.InstitutionHierarchy
{
    public class InstitutionCreateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
    }
}
