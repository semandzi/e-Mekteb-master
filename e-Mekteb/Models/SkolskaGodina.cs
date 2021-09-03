using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class SkolskaGodina
    {
        public int SkolskaGodinaId { get; set; }

        public virtual List<Aktivnost> Aktivnosti { get; set; }
        public virtual List<Razred> Razredi { get; set; }

        [Required(ErrorMessage ="Godina je obavezno polje")]
        [MaxLength(9)]
        [MinLength(9)]

        public string Godina { get; set; }
    }
}
