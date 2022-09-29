using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class StudentNote
    {
        public StudentNote()
        {
            Ucenici = new List<AplicationUser>();
            Biljeske = new List<Biljeska>();
        }

        public List<Biljeska >Biljeske { get; set; }
        public List<AplicationUser> Ucenici { get; set; }

       
    }
}
