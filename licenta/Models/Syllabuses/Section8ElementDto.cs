namespace licenta.Models.Syllabuses
{
    public class Section8ElementDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public string Note { get; set; } = string.Empty;
    }
}
