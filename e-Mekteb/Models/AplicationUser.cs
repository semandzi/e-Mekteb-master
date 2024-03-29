﻿using e_Mekteb.ViewModel;
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

       
        public string AplicationUserId { get; set; }

        public virtual List<UcenikAktivnost> UcenickeAktivnosti { get; set; }
        public virtual List<VjerouciteljAktivnost> VjerouciteljskeAktivnosti { get; set; }

        public List<AplicationUser> Ucenici { get; set; }

        public AplicationUser()
        {
            Ucenici = new List<AplicationUser>();


        }



        [StringLength(50)]
        [Display(Name = "Ime i Prezime")]
        public string ImeiPrezime { get; set; }


       

        public Spol Spol { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Grad")]
        public string NazivMjesta { get; set; }

       
        [StringLength(100)]
        public string Ulica { get; set; }

        
        [MaxLength(5)]
        [MinLength(5)]
        [Display(Name = "Poštanski broj")]

        public string PostanskiBroj { get; set; }



        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DatumRodenja { get; set; }


        

        //[Required(ErrorMessage = "Ime i oca je obavezno polje")]
        [StringLength(50)]
        [Display(Name = "Ime i prezime roditelja")]
        public string ImeiPrezimeRoditelja{ get; set; }

        [Display(Name = "Broj mobitela od roditelja")]
        public string BrojMobitela { get; set; }

    }
    public enum Spol
    {
        Muško,
        Žensko
    }




















}























