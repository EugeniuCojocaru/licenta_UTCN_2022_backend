namespace licenta.Entities
{
    public class Section2Teacher
    {
        public Guid Section2Id { get; set; }
        public Section2 Section2 { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
