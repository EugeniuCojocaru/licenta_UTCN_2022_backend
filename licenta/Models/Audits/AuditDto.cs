namespace licenta.Models.Audits
{
    public class AuditDto
    {
        public string User { get; set; }
        public string Operation { get; set; }
        public string Entity { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
