using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Medzlis
    {
        public int MedzlisId { get; set; }
        public virtual List<ClanMualimskogVijeca> ClanoviMualimskogVijeca { get; set; }
        public virtual List<Razred> Razredi { get; set; }
        public virtual List<VjerouciteljViewModel> Vjeroucitelji { get; set; }


        public virtual Adresa Adresa { get; set; }

        [Display(Name = "Grad")]
        public int AdresaId { get; set; }


        [Required(ErrorMessage ="Naziv je obavezno polje")]
        [StringLength(100)]
        public string Naziv { get; set; }



        [Required(ErrorMessage = "Kontakt je obavezno polje")]
        [MinLength(10)]
        [MaxLength(12)]
        public string Kontakt { get; set; }






        [Required(ErrorMessage = "Glavni Imam je obavezno polje")]
        [StringLength(50)]
        [Display(Name ="Glavni Imam")]
        public string GlavniImam { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje")]
        [StringLength(254)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        public string EmailGlavnogImama { get; set; }



        





    }
}
