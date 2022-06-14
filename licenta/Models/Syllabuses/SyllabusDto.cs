using licenta.Entities;

namespace licenta.Models.Syllabuses
{
    public class SyllabusDto
    {
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        public Guid? Section1Id { get; set; }
        public Guid? Section2Id { get; set; }
    }
}
