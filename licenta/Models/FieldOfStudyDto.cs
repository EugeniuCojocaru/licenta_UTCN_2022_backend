using System.ComponentModel.DataAnnotations;
namespace licenta.Models
{
    public class FieldOfStudyDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the department for the field of study")]
        public Guid DepartmentId { get; set; }

    }
}
