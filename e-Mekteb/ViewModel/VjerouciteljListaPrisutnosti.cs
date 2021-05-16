using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class VjerouciteljListaPrisutnosti
    {
        public VjerouciteljListaPrisutnosti()
        {
            
            UceniciIsSelected= new List<PrisutnostVjeroucitelj>();
        }
        public Prisutnost TempPrisutnost { get; set; }
        public List<PrisutnostVjeroucitelj>UceniciIsSelected { get; set; }
        public bool OdaberiSve { get; set; }
    }
}
