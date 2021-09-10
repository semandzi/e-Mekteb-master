using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class VjerouciteljAktivnost
    {   [Key]
        public int VjerouciteljAktivnostId { get; set; }
        public int AktivnostId { get; set; }
        public string NazivPredmeta { get; set; }

        public  virtual AplicationUser Vjeroucitelj { get; set; }
        public string VjerouciteljId { get; set; }

        public virtual Aktivnost  Aktivnost { get; set; }
    }
}
