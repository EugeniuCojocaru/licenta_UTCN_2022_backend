using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Institution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

        public Institution(string name)
        {
            Name = name;
        }
        public Institution()
        {

        }
    }
}
