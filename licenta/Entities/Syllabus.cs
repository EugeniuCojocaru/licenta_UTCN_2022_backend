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

        [ForeignKey("Section1Id")]
        public Section1? Section1 { get; set; }
        public Guid? Section1Id { get; set; }

        [ForeignKey("Section2Id")]
        public Section2? Section2 { get; set; }
        public Guid? Section2Id { get; set; }

        [ForeignKey("Section3Id")]
        public Section3? Section3 { get; set; }
        public Guid? Section3Id { get; set; }

        [ForeignKey("Section4Id")]
        public Section4? Section4 { get; set; }
        public Guid? Section4Id { get; set; }

        [ForeignKey("Section5Id")]
        public Section5? Section5 { get; set; }
        public Guid? Section5Id { get; set; }

        [ForeignKey("Section6Id")]
        public Section6? Section6 { get; set; }
        public Guid? Section6Id { get; set; }

        [ForeignKey("Section7Id")]
        public Section7? Section7 { get; set; }
        public Guid? Section7Id { get; set; }

        [ForeignKey("Section8Id")]
        public Section8? Section8 { get; set; }
        public Guid? Section8Id { get; set; }

        [ForeignKey("Section9Id")]
        public Section9? Section9 { get; set; }
        public Guid? Section9Id { get; set; }

        [ForeignKey("Section10Id")]
        public Section10? Section10 { get; set; }
        public Guid? Section10Id { get; set; }
    }
}
