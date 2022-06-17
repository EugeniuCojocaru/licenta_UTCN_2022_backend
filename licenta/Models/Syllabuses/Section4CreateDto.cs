namespace licenta.Models.Syllabuses
{
    public class Section4CreateDto
    {
        public List<string>? Compentences { get; set; } = new List<string>();
        public List<Guid>? Subjects { get; set; } = new List<Guid>();
    }
}
