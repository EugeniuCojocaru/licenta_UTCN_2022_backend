using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }
        public Guid FacultyId { get; set; }

        public ICollection<FieldOfStudy> FieldsOfStudy { get; set; } = new List<FieldOfStudy>();

        public Department(string name)
        {
            Name = name;
        }

        public Department()
        {
        }
    }
}
