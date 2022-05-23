using System.ComponentModel.DataAnnotations;

namespace licenta.Models.InstitutionHierarchy
{
    public class FieldOfStudyCreateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the department for the field of study")]
        public Guid DepartmentId { get; set; }
    }
}
