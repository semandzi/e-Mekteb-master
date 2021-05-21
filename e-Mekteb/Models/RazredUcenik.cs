using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class RazredUcenik
    {
        public int RazredUcenikId { get; set; }
        public string VjerouciteljId { get; set; }
        public string UcenikId { get; set; }
        public int MedzlisId { get; set; }
        public int SkolaId { get; set; }
        public int GodinaId { get; set; }
    }
}
