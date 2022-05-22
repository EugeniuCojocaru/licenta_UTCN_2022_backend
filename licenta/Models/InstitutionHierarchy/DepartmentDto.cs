namespace licenta.Models
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<FieldOfStudyDto> FieldsOfStudy { get; set; } = new List<FieldOfStudyDto>();
    }
}
