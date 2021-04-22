using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class VjerouciteljUcenik
    {
        public int VjerouciteljUcenikId { get; set; }
        public virtual AplicationUser Ucenik { get; set; }
        public virtual string UcenikId { get; set; }
        public virtual AplicationUser Vjeroucitelj { get; set; }
        public string VjerouciteljId { get; set; }

    }
}
