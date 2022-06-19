using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Subjects
{
    public class SubjectUpdateDto
    {
        [Required(ErrorMessage = "You should provide the subject's id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You should provide a name")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "You should provide the subject's code")]
        public string Code { get; set; } = String.Empty;
    }
}
