namespace licenta.Models.Subjects
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public bool HasSyllabus { get; set; } = true;
    }
}
