using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class AplicationUser:IdentityUser
    {
        public int AplicationUserId { get; set; }

        public virtual List<UcenikAktivnost> UcenickeAktivnosti { get; set; }
        public virtual List<VjerouciteljAktivnost> VjerouciteljskeAktivnosti { get; set; }






        
        public string ImeIPrezime { get; set; }





        //[Required(ErrorMessage = "Datum Rođenja je obavezno polje")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum Rođenja")]
        public DateTime DatumRodenja { get; set; }

        public Spol Spol { get; set; }

        public int Starost { get; set; }

        //public virtual Adresa Adresa { get; set; }
        //public int AdresaId { get; set; }


        //[Required(ErrorMessage = "Ime Oca je obavezno polje")]
        [MaxLength(50)]
        [Display(Name = "Ime i Prezime Oca")]
        public string ImeOca { get; set; }





        


    }

    public enum Spol
    {
        Muško,
        Žensko
    }





















}
