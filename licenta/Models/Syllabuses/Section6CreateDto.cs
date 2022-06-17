using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section6CreateDto
    {
        [Required(ErrorMessage = "You should provide professional competences")]
        public List<string>? Professional { get; set; } = new List<string>();
        public List<string>? Cross { get; set; } = new List<string>();
    }
}
