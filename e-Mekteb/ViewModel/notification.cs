using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class Notification
    {
        public Notification()
        {
            obavijesti = new List<Obavijest>();
            VjerouciteljiNaObavijestima = new List<AplicationUser>();

        }
        public List<Obavijest> obavijesti { get; set; }
        public List<AplicationUser> VjerouciteljiNaObavijestima { get; set; }
    }
}
