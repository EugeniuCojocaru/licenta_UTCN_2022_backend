namespace licenta.Models.Syllabuses
{
    public class Section8Dto
    {
        public Guid Id { get; set; }
        public List<string>? TeachingMethodsCourse { get; set; } = new List<string>();

        public List<string>? TeachingMethodsLab { get; set; } = new List<string>();

        public List<string>? BibliographyCourse { get; set; } = new List<string>();

        public List<string>? BibliographyLab { get; set; } = new List<string>();

        public List<Section8ElementDto>? LecturesCourse { get; set; } = new List<Section8ElementDto>();

        public List<Section8ElementDto>? LecturesLab { get; set; } = new List<Section8ElementDto>();
    }
}
