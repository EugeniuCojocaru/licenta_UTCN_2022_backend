using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Faculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("InstitutionId")]
        public Institution? Institution { get; set; }
        public Guid InstitutionId { get; set; }

        public ICollection<Department> Department { get; set; } = new List<Department>();

        public Faculty(string name)
        {
            Name = name;
        }
    }
}
