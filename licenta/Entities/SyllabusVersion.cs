using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class SyllabusVersion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;


        [Required]
        [ForeignKey("SyllabusId")]
        public Syllabus? Syllabus { get; set; }
        public Guid SyllabusId { get; set; }


    }
}
