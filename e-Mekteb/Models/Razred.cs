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

        [Display(Name = "Školska Godina")]
        public virtual SkolskaGodina SkolskaGodina { get; set; }
        [Display(Name = "Školska Godina")]
        public int SkolskaGodinaId { get; set; }






        [Display(Name ="Medžlis")]
        public virtual Medzlis Medzlis { get; set; }
        [Display(Name = "Medžlis")]
        public int MedzlisId { get; set; }



        [Display(Name = "Vjeroučitelj")]
        public virtual AplicationUser Vjeroucitelj { get; set; }
        [Display(Name = "Vjeroučitelj")]
        [ForeignKey("Vjeroucitelj")]
        public string  VjerouciteljId{ get; set; }





        [Required(ErrorMessage = "Razred je obavezno polje")]
        [StringLength(50)]
        [Display(Name ="Razred")]
        public string Naziv { get; set; }


    }
}
