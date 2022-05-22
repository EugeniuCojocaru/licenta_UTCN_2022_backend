using System.ComponentModel.DataAnnotations;
namespace licenta.Models
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the faculty for the department")]
        public Guid FacultyId { get; set; }
    }
}
