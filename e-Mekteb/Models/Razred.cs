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

        public virtual SkolskaGodina SkolskaGodina { get; set; }

        [Display(Name = "Odaberi godinu")]
        public int SkolskaGodinaId { get; set; }


        public virtual Medzlis Medzlis { get; set; }

        [Display(Name ="Odaberi medžlis")]
        public int MedzlisId { get; set; }


        public virtual AplicationUser AplicationUser { get; set; }

        [Display(Name = "Odaberi vjeroučitelja")]
        public int AplicationUserId { get; set; }



        [Required(ErrorMessage = "Razred je obavezno polje")]
        [StringLength(50)]
        [Display(Name ="Razred")]
        public string Naziv { get; set; }


    }
}
