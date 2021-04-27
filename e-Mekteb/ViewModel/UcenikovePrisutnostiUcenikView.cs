using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class UcenikovePrisutnostiUcenikView
    {
        public UcenikovePrisutnostiUcenikView()
        {
            UcenikovePrisutnosti = new List<Prisutnost>();
            UcenikoveAktivnosti = new List<Aktivnost>();
        }

        public List<Prisutnost> UcenikovePrisutnosti { get; set; }
        public List<Aktivnost> UcenikoveAktivnosti { get; set; }
    }
}
