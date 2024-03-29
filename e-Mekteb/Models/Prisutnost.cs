﻿using e_Mekteb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Prisutnost

    {
        public int PrisutnostId { get; set; }

        [Required(ErrorMessage = "Datum je obavezno polje")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]

        public DateTime Datum { get; set; }


        public  AplicationUser AplicationUser { get; set; }

        [Display(Name = "Učenik")]
        public string AplicationUserId { get; set; }



        public virtual Aktivnost Aktivnost { get; set; }
        [Display(Name = "Odaberi aktivnost")]
        public int AktivnostId { get; set; }



        [Display(Name = "Prisutnost")]
        public string IsPrisutan { get; set; }

    }
    public enum IsPrisutan
    {
        Da,
        Ne
    }
}