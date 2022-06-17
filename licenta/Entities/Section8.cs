using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section8
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string TeachingMethodsCourse { get; set; } = string.Empty;
        [Required]
        public string TeachingMethodsLab { get; set; } = string.Empty;
        [Required]
        public string BibliographyCourse { get; set; } = string.Empty;
        [Required]
        public string BibliographyLab { get; set; } = string.Empty;

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }

        public ICollection<Section8Element> Lectures { get; set; } = new List<Section8Element>();
    }
}
