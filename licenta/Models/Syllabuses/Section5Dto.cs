﻿namespace licenta.Models.Syllabuses
{
    public class Section5Dto
    {
        public Guid Id { get; set; }
        public List<string>? Course { get; set; } = new List<string>();
        public List<string>? Application { get; set; } = new List<string>();
    }
}
