using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class UcenikAktivnost
    {
        public int UcenikAktivnostId { get; set; }

        public virtual Aktivnost Aktivnost { get; set; }
        [ForeignKey("Aktivnost")]
        public int AktivnostId { get; set; }
        public virtual AplicationUser Ucenik { get; set; }
        [ForeignKey("Ucenik")]
        public string UcenikId { get; set; }
       
        public string VjerouciteljId { get; set; }

        public string NazivPredmeta { get; set; }






    }
}