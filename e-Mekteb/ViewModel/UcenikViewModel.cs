using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class UcenikViewModel
    {
        public int UcenikViewModelId { get; set; }


        public virtual Medzlis Medzlis { get; set; }
        [Display(Name = "Medžlis")]
        public int MedzlisId { get; set; }


        public virtual Adresa Adresa { get; set; }
        [Display(Name = "Boravište")]
        public int AdresaId { get; set; }



        [Required(ErrorMessage = "Ime i Prezime je obavezno polje")]
        [StringLength(50)]
        public string ImeiPrezime { get; set; }

        public Spol Spol { get; set; }


        [Required(ErrorMessage = "Datum Rođenja je obavezno polje")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum Rođenja")]
        public DateTime DatumRodenja { get; set; }


        public int Starost { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
