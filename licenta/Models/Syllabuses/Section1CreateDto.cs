using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section1CreateDto
    {


        [Required(ErrorMessage = "You should provide a cycle of study")]
        public string CycleOfStudy { get; set; } = "Bachelor of Science";
        [Required(ErrorMessage = "You should provide a program of study")]
        public string ProgramOfStudy { get; set; } = "Computer science";
        [Required(ErrorMessage = "You should provide the qualification")]
        public string Qualification { get; set; } = "Engineer";
        [Required(ErrorMessage = "You should provide the form of education")]
        public string FormOfEducation { get; set; } = "Full time";

        [Required(ErrorMessage = "You should provide an institution")]
        public Guid InstitutionId { get; set; }
        [Required(ErrorMessage = "You should provide a faculty")]
        public Guid FacultyId { get; set; }
        [Required(ErrorMessage = "You should provide a department")]
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "You should provide a field of study")]
        public Guid FieldOfStudyId { get; set; }

    }
}
/*
 {
  "institutionId": "fd8b2479-c33d-4e4b-8f86-b58b62269ba1",
  "facultyId": "a348f759-2e1e-438f-b0af-f55a543d50c2",
  "departmentId": "088a2192-a6a6-41af-8b44-0e33a62b49e0",
  "fieldOfStudyId": "01c6a23a-d22f-4c35-9881-d7c12d297167"
}*/