using System.ComponentModel.DataAnnotations;
using static licenta.Entities.Constants;

namespace licenta.Models.Teachers
{
    public class TeacherCreateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "You should provide an email")]
        public string Email { get; set; } = String.Empty;

        public Role Role { get; set; } = Role.User;
    }
}
