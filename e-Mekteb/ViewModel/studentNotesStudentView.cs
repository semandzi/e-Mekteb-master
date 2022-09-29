using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class StudentNotesStudentView
    {
        public StudentNotesStudentView()
        {
            UcenikoveBiljeske = new List<Biljeska>();
            UcenikoveAktivnosti = new List<Aktivnost>();
        }

        public List<Biljeska> UcenikoveBiljeske { get; set; }
        public List<Aktivnost> UcenikoveAktivnosti { get; set; }
    }
}
