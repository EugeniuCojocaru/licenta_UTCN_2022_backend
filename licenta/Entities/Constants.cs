namespace licenta.Entities
{
    public class Constants
    {
        public enum Role { Admin = 10, User = 0 };
        public enum TypeOfAssessment { Exam = 0, Colloquium = 1, Verification = 2 }
        public enum SubjectCategory1
        {
            Fundamental = 0, Domain = 1, Speciality = 2, Complementary = 3
        }
        public enum SubjectCategory2 { Mandatory = 0, Optional = 1, Dispensable = 2 }
    }
}
