namespace licenta.Models.Syllabuses
{
    public class SyllabusVersionDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

        public Guid SyllabusId { get; set; }
    }
}
