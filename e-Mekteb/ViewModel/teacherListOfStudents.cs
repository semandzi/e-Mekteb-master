using System.Collections.Generic;

namespace e_Mekteb.ViewModel
{
    public class TeacherListOfStudents
    {
        public TeacherListOfStudents() {
            List<StudentProfilFlag> Profili = new List<StudentProfilFlag>();
        }
        public int SkolskaGodinaId { get; set; }
        public List<StudentProfilFlag> Profili{ get; set; }
    }
}
