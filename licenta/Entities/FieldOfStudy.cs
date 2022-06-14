using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class FieldOfStudy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; }

        public FieldOfStudy(string name)
        {
            Name = name;
        }

        public FieldOfStudy()
        {
        }
    }
}
