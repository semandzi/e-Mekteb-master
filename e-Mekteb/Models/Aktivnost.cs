﻿using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Aktivnost
    {
        

        public int AktivnostId { get; set; }

        public virtual List<UcenikAktivnost> UcenickeAktivnosti { get; set; }
        public virtual List<VjerouciteljAktivnost> VjerouciteljskeAktivnosti { get; set; }
        public virtual List<Biljeska> Biljeske { get; set; }
        public virtual List<Prisutnost> Prisutnosti { get; set; }

        [Display(Name = "Školska Godina")]
        public virtual SkolskaGodina SkolskaGodina { get; set; }
        [Display(Name = "Školska Godina")]
        public int SkolskaGodinaId { get; set; }


       
       
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Display(Name = "Segmenti Nastave")]

        public TipAktivnosti TipAktivnosti { get; set; }



    }
    public enum TipAktivnosti
    {
        Teorija,
        Praktično,
        Sve
    };
}
