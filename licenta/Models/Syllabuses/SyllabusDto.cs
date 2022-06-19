using licenta.Models.Subjects;

namespace licenta.Models.Syllabuses
{
    public class SyllabusDto
    {
        public Guid Id { get; set; }
        public SubjectDto Subject { get; set; }
        public Section1Dto Section1 { get; set; }
        public Section2Dto Section2 { get; set; }
        public Section3Dto Section3 { get; set; }
        public Section4Dto Section4 { get; set; }
        public Section5Dto Section5 { get; set; }
        public Section6Dto Section6 { get; set; }
        public Section7Dto Section7 { get; set; }
        public Section8Dto Section8 { get; set; }
        public Section9Dto Section9 { get; set; }
        public Section10Dto Section10 { get; set; }

    }
}
