using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models
{
    public class Skola
    {

        public int SkolaId { get; set; }

        public virtual VjerouciteljViewModel VjerouciteljViewModel { get; set; }
        [Display(Name ="Odaberi vjeroucitelja")]
        public int VjerouciteljViewModelId { get; set; }

        public string NazivSkole { get; set; }


    }
}
