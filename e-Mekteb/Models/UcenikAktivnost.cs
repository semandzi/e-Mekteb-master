using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class UcenikAktivnost
    {
        public int UcenikAktivnostId { get; set; }
        public int AplicationUserId { get; set; }
        public int AktivnostId { get; set; }
        public virtual AplicationUser AplicationUser { get; set; }
        public virtual Aktivnost Aktivnost { get; set; }


    }
}
