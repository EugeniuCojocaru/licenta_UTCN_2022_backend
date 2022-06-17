using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Section8Element
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public bool IsCourse { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Duration { get; set; } = 0;
        public string Note { get; set; } = string.Empty;


        [ForeignKey("Section8Id")]
        public Section8? Section8 { get; set; }
        public Guid Section8Id { get; set; }
    }
}
