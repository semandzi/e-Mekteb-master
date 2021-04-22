using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class ClanMualimskogVijeca
    {
        public int ClanMualimskogVijecaId { get; set; }



        [Display(Name = "Medžlis")]
        public virtual Medzlis Medzlis { get; set; }
        [Display(Name = "Medžlis")]
        public int MedzlisId { get; set; }



        
        [Required(ErrorMessage ="Ime i Prezime je obavezno polje")]
        [StringLength(50)]
        [Display(Name = "Ime i Prezime")]
        public string ImeIPrezimeClanaVijeca { get; set; }


        [Required(ErrorMessage = "Email je obavezno polje")]
        [EmailAddress(ErrorMessage ="Neispravan format email adrese")]
        [Display(Name = "Email")]
         public string EmailClanaVijeca { get; set; }


        [Required(ErrorMessage = "Mobitel je obavezno polje")]
        [MinLength(10)]
        [MaxLength(12)]
        [Display(Name = "Mobitel")]
        public string KontaktClanaVijeca { get; set; }
    }
}

