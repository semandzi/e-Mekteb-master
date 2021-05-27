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
        public Medzlis Medzlis { get; set; }
        [ForeignKey("Medzlis")]
        public int MedzlisId { get; set; }
        public string VjerouciteljId { get; set; }
        public string NazivSkole { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
        public int PostanskiBroj { get; set; }





    }
}
