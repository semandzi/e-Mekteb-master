using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class StudentPresence
    {
        public StudentPresence()
        {
            Ucenici = new List<AplicationUser>();
            Prisutnosti = new List<Prisutnost>();
        }

        public List<Prisutnost> Prisutnosti { get; set; }
        public List<AplicationUser> Ucenici { get; set; }
    }
}
