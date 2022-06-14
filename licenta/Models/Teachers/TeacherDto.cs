using licenta.Entities;
using static licenta.Entities.Constants;

namespace licenta.Models.Teachers
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public Role Role { get; set; }
        public IEnumerable<Section2Teacher> Section2Teacher { get; set; } = new List<Section2Teacher>();

    }
}
