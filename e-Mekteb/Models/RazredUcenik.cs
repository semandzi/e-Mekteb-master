using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class RazredUcenik
    {
        public int RazredUcenikId { get; set; }
        public string Razred { get; set; }
        public string VjerouciteljId { get; set; }
        public string UcenikId { get; set; }
        public int MedzlisId { get; set; }
        public Skola Skola { get; set; }
        public int SkolaId { get; set; }
        public SkolskaGodina SkolskaGodina { get; set; }
        [ForeignKey("SkolskaGodina")]
        public int SkolskaGodinaId { get; set; }

        [Required(ErrorMessage = "Datum je obavezno polje")]
        [DataType(DataType.Date)]
        public DateTime DatumUpisa { get; set; }

        [Required(ErrorMessage = "Datum je obavezno polje")]
        [DataType(DataType.Date)]
        public DateTime DatumIspisa { get; set; }
    }
}
