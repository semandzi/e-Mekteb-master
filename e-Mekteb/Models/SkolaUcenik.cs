using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class SkolaUcenik
    {
        public int SkolaUcenikId { get; set; }
        public string UcenikId { get; set; }
        public string NazivSkole { get; set; }
        public string VjerouciteljId { get; set; }
        public Skola Skola { get; set; }
        [ForeignKey("Skola")]
        public int SkolaId { get; set; }
    }
}
