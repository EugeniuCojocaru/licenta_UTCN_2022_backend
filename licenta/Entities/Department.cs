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
        public string Name { get; set; }

        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }
        public Guid FacultyId { get; set; }

        public ICollection<StudyDomain> StudyDomains { get; set; } = new List<StudyDomain>();  
       
        public Department(string name)
        {
            Name = name;
        }
    }
}
