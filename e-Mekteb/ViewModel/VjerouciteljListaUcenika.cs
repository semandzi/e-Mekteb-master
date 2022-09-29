using System.Collections.Generic;

namespace e_Mekteb.ViewModel
{
    public class VjerouciteljListaUcenika
    {
        public VjerouciteljListaUcenika() {
            List<UcenikProfilFlag> Profili = new List<UcenikProfilFlag>();
        }
        public int SkolskaGodinaId { get; set; }
        public List<UcenikProfilFlag> Profili{ get; set; }
    }
}
