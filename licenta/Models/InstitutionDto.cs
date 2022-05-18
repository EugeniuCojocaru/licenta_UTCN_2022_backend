namespace licenta.Models
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;


        public ICollection<FacultyDto> Faculties { get; set; } = new List<FacultyDto>();



    }
}
