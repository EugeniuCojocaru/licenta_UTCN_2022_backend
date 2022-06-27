using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static licenta.Entities.Constants;

namespace licenta.Entities
{
    public class Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public User? User { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public Operation Operation { get; set; }
        [Required]
        public EntityNames Entity { get; set; }

        public string Notes { get; set; } = string.Empty;


    }
}
