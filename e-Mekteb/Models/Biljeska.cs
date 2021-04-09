using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Biljeska
    {

        public int BiljeskaId { get; set; }

        [Required(ErrorMessage = "Datum je obavezno polje")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }


        public virtual UcenikViewModel UcenikViewModel { get; set; }
        [Display(Name ="Odaberi učenika")]
        public int UcenikViewModelId { get; set; }




        public virtual Aktivnost Aktivnost { get; set; }
        [Display(Name = "Odaberi aktivnost")]
        public int AktivnostId { get; set; }




        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bilješke su obavezno polje")]
        [StringLength(200)]
        [Display(Name = "Bilješke")]

        public string Biljeske { get; set; }

    }
}
