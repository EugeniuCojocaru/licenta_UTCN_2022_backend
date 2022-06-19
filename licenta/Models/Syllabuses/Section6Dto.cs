namespace licenta.Models.Syllabuses
{
    public class Section6Dto
    {
        public Guid Id { get; set; }
        public List<string>? Professional { get; set; } = new List<string>();
        public List<string>? Cross { get; set; } = new List<string>();
    }
}
