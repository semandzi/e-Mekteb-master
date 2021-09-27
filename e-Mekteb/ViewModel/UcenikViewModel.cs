using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class UcenikViewModel
    {
        public UcenikViewModel()
        {
            MedzlisiUcenika =new List<Medzlis>();
        }

        public int UcenikViewModelId { get; set; }


        public List<Medzlis> MedzlisiUcenika { get; set; }




        public Medzlis Medzlis { get; set; }
        [Display(Name = "Medžlis")]
        [ForeignKey("Medzlis")]
        public int MedzlisId { get; set; }

        public string userId { get; set; }

        [Required(ErrorMessage = "Ime i Prezime je obavezno polje")]
        [StringLength(50)]
        [Display(Name = "Ime i Prezime")]

        public string ImeiPrezime { get; set; }



        [Required(ErrorMessage = "Spol je obavezno polje")]
        public Spol Spol { get; set; }


        [Required(ErrorMessage = "Grad je obavezno polje")]
        [StringLength(50)]
        [Display(Name = "Grad")]
        public string NazivMjesta { get; set; }

        [StringLength(100)]
        public string Ulica { get; set; }

        [MaxLength(5)]
        [MinLength(5)]
        [Display(Name = "Poštanski broj")]
        public string PostanskiBroj { get; set; }


        


        [DataType(DataType.Date)]
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatumRodenja { get; set; }



        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Ime i prezime roditelja")]
        public string ImeiPrezimeRoditelja { get; set; }


        [Display(Name = "Broj mobitela od roditelja")]
        [MaxLength(12)]
        public string BrojMobitela{ get; set; }



    }




}
