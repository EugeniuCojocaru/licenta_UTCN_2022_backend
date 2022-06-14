using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Syllabus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }
        public Guid SubjectId { get; set; }

        [Required]
        [ForeignKey("Section1Id")]
        public Section1? Section1 { get; set; }
        public Guid Section1Id { get; set; }

        [Required]
        [ForeignKey("Section2Id")]
        public Section2? Section2 { get; set; }
        public Guid Section2Id { get; set; }
    }
}
