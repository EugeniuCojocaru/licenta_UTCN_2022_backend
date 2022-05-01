namespace licenta.Models
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public ICollection<FacultyDto> Faculties { get; set; } = new List<FacultyDto>();



    }
}
