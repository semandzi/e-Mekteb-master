using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class VjerouciteljAktivnost
    {
        public int VjerouciteljAktivnostId { get; set; }
        public string AplicationUserId { get; set; }
        public int AktivnostId { get; set; }
        public string NazivPredmeta { get; set; }

        public  virtual AplicationUser Vjeroucitelj { get; set; }
        public virtual Aktivnost  Aktivnost { get; set; }
    }
}
