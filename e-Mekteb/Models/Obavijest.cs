using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Obavijest
    {
        public int ObavijestId { get; set; }
        public string Naslov { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Sadržaj je obavezno polje")]
        [StringLength(500)]
        [Display(Name = "Sadržaj")]
        public string Sadrzaj { get; set; }
        public AplicationUser Vjeroucitelj { get; set; }
        [ForeignKey("Vjeroucitelj")]
        public string VjerouciteljId { get; set; }

    }
}
