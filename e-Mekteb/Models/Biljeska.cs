﻿using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Biljeska
    {
        

        public int BiljeskaId { get; set; }

       

        [Required(ErrorMessage = "Datum je obavezno polje")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }


        public AplicationUser AplicationUser { get; set; }
        [Display(Name ="Učenik")]
        [ForeignKey("AplicationUser")]
        public string AplicationUserId { get; set; }




        public virtual Aktivnost Aktivnost { get; set; }
        [Display(Name = "Aktivnost")]
        [ForeignKey("AktivnostId")]

        public int AktivnostId { get; set; }




        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bilješke su obavezno polje")]
        [StringLength(200)]
        [Display(Name = "Bilješka")]

        public string Biljeske { get; set; }

    }
}
