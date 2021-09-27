using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using e_Mekteb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_Mekteb.ViewModel
{
    public class FilterViewModel
    {
        //public FilterViewModel()
        //{
        //    List<SelectList> ListaMedzlisa= new List<SelectList>();
        //    List<SelectList> MedzlisiId = new List<SelectList>();
        //}
        public int SkolaId { get; set; }
        public int RazredId { get; set; }
        public int GodinaId { get; set; }
        public int MedzlisId { get; set; }
        public string VjerouciteljId{ get; set; }
        public List<SelectList> ListaMedzlisa { get; set; }
        public List<SelectList> MedzlisiId  { get; set; }
    }
}
