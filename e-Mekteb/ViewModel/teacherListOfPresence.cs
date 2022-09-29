using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class TeacherListOfPresence
    {
        public TeacherListOfPresence()
        {
            
            Schools= new List<School>();
        }
        public bool OdaberiSve { get; set; }
        public Prisutnost TempPrisutnost { get; set; }
        public List<School> Schools { get; set; }
    }


    public class School {
        public string SchoolName { get; set; }
        public List<StudentInfo> StudentInfoList { get; set; }
    }
}
