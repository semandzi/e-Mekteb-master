using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class SkoleLista
    {
        public SkoleLista()
        {
            Skole = new List<SkolaUcenikView>();
        }
        public List<SkolaUcenikView> Skole { get; set; }
        public string IsSelected { get; set; }

    }
}
