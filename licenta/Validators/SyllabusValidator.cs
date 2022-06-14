namespace licenta.Validators
{
    public class SyllabusValidator
    {


        /* public SyllabusValidator(IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository, IInstitutionRepository institutionRepository, IFieldOfStudyRepository fieldOfStudyRepository)
         {
             _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
             _facultyRepository = facultyRepository ?? throw new ArgumentNullException(nameof(facultyRepository));
             _institutionRepository = institutionRepository ?? throw new ArgumentNullException(nameof(institutionRepository));
             _fieldOfStudyRepository = fieldOfStudyRepository ?? throw new ArgumentNullException(nameof(fieldOfStudyRepository));
         }

         private async Task<bool> ValidateSection1(Section1CreateDto newEntry)
         {
             IInstitutionRepository _institutionRepository = new InstitutionRepository();
             if (newEntry == null) return false;
             if (await _institutionRepository.Exists(newEntry.InstitutionId) == false)
                 return false;
             if (await _facultyRepository.Exists(newEntry.FacultyId) == false)
                 return false;
             if (await _departmentRepository.Exists(newEntry.DepartmentId) == false)
                 return false;
             if (await _fieldOfStudyRepository.Exists(newEntry.FieldOfStudyId) == false)
                 return false;

             return true;
         }

         private async Task<bool> ValidateSection2(Section1CreateDto newEntry)
         {
             if (newEntry == null) return false;
             if (await _institutionRepository.Exists(newEntry.InstitutionId) == false)
                 return false;
             if (await _facultyRepository.Exists(newEntry.FacultyId) == false)
                 return false;
             if (await _departmentRepository.Exists(newEntry.DepartmentId) == false)
                 return false;
             if (await _fieldOfStudyRepository.Exists(newEntry.FieldOfStudyId) == false)
                 return false;

             return true;
         }*/
    }
}
