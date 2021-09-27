using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class UcenikProfilFlag
    {


        public AplicationUser AplicationUser { get; set; }
        public int Flag { get; set; }
        
        public DateTime Datum { get; set; }
        public string Razred { get; set; }
        public string LokacijaNastave { get; set; }
    }
}
