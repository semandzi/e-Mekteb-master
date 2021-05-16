using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Razred
    {
        public int RazredId { get; set; }

       





        [Required(ErrorMessage = "Razred je obavezno polje")]
        [StringLength(50)]
        [Display(Name ="Razred")]
        public string Naziv { get; set; }


    }
}
