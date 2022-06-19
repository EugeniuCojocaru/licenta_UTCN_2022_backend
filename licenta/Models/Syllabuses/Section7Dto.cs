namespace licenta.Models.Syllabuses
{
    public class Section7Dto
    {
        public Guid Id { get; set; }
        public string GeneralObjective { get; set; } = string.Empty;
        public List<string>? SpecificObjectives { get; set; } = new List<string>();
    }
}
