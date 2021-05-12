using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class AktivnostiUcenika
    {
        public string UcenikId { get; set; }
        public int AktivnostId { get; set; }
        public string VjerouciteljId { get; set; }

        public string NazivPredmeta { get; set; }
        public bool IsSelected { get; set; }
    }
}
