﻿using licenta.Entities;
using licenta.Models.Teachers;
using static licenta.Entities.Constants;

namespace licenta.Models.Syllabuses
{
    public class Section2Dto
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public int YearOfStudy { get; set; }
        public int Semester { get; set; }
        public TypeOfAssessment Assessment { get; set; }
        public SubjectCategory1 Category1 { get; set; }
        public SubjectCategory2 Category2 { get; set; }
        public TeacherDto? Lecturer { get; set; }
        public List<Section2Teacher>? Section2Teacher { get; set; } = new List<Section2Teacher>();
    }
}