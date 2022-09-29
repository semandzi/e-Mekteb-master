using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class SchoolList
    {
        public SchoolList()
        {
            Skole = new List<StudentSchool>();
        }
        public List<StudentSchool> Skole { get; set; }
        public string IsSelected { get; set; }

    }
}
