namespace licenta.Models
{
    public class FacultyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();

    }
}
