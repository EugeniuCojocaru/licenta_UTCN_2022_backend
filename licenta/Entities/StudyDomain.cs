using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace licenta.Entities
{
    public class StudyDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public Guid DepartmentId { get; set; } 
        public StudyDomain(string name)
        {
            Name = name;
        }
    }
}
