using licenta.Models.Subjects;

namespace licenta.Models.Syllabuses
{
    public class Section4Dto
    {
        public Guid Id { get; set; }
        public List<string>? Compentences { get; set; } = new List<string>();
        public List<SubjectDto>? Subjects { get; set; } = new List<SubjectDto>();
    }
}
