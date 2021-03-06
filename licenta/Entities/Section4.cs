using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section4
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Competences { get; set; } = string.Empty;

        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }
    }
}
