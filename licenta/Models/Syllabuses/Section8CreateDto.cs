using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section8CreateDto
    {
        [Required(ErrorMessage = "You should provide the general objective")]
        public string GeneralObjective { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should provide the specific objectives")]
        public List<string>? SpecificObjectives { get; set; } = new List<string>();
    }
}
