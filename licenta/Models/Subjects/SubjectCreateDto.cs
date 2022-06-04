using System.ComponentModel.DataAnnotations;
namespace licenta.Models.Subjects
{
    public class SubjectCreateDto
    {

        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "You should provide the subject's code code")]
        public string Code { get; set; } = String.Empty;
    }
}
