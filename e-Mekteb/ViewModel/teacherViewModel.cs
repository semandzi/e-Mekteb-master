using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class TeacherViewModel
    {       
        public int VjerouciteljViewModelId { get; set; }
        public virtual List<Aktivnost> Aktivnosti { get; set; }
        public virtual List<Razred> Razredi { get; set; }
        public virtual List<Skola> Skole { get; set; }

        public Medzlis Medzlis { get; set; }
        [Display(Name = "Medžlis")]
        [ForeignKey("Medzlis")]
        public int MedzlisId { get; set; }

        [Required(ErrorMessage = "Ime i Prezime je obavezno polje")]
        [StringLength(50)]
        public string ImeiPrezime { get; set; }

        public Spol Spol { get; set; }

        [Required(ErrorMessage = "Grad je obavezno polje")]
        [StringLength(50)]
        [Display(Name = "Grad")]
        public string NazivMjesta { get; set; }

        [Required(ErrorMessage = "Ulica je obavezno polje")]
        [StringLength(100)]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Poštanski broj je obavezno polje")]
        [MaxLength(5)]
        [MinLength(5)]
        [Display(Name = "Poštanski broj")]

        public string PostanskiBroj { get; set; }


        [Required(ErrorMessage = "Datum Rođenja je obavezno polje")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum Rođenja")]
        public DateTime DatumRodenja { get; set; }


        public int Starost { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ime oca je obavezno polje")]
        [StringLength(50)]
        public string ImeOca { get; set; }







    }

}
