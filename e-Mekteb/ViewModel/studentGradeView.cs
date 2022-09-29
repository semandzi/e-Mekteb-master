using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class StudentGradeView
    {
        public StudentGradeView()
        {
            Razredi = new List<RazredUcenikView>();
        }
        public string IsSelected { get; set; }

        [Display(Name ="Odaberi Godinu")]
        public int SkolskaGodinaId { get; set; }

        public List<RazredUcenikView> Razredi{ get; set; }
    }
}
