using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Adresa

    {
        public int AdresaId { get; set; }


        [Required(ErrorMessage = "Grad je obavezno polje")]
        [StringLength(50)]
        [Display(Name ="Grad")]
        public string NazivMjesta { get; set; }

        [Required(ErrorMessage = "Ulica je obavezno polje")]
        [StringLength(100)]
        public string Ulica { get; set; }

        [Required(ErrorMessage ="Poštanski broj je obavezno polje")]
        [MaxLength(5)]
        [MinLength(5)]
        [Display(Name = "Poštanski broj")]

        public string PostanskiBroj { get; set; }





    }
}
