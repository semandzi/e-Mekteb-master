using e_Mekteb.ViewModel;
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
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }


        public virtual UcenikViewModel UcenikViewModel { get; set; }

        [Display(Name = "Odaberi učenika")]
        public int UcenikViewModelId { get; set; }



        public virtual Aktivnost Aktivnost { get; set; }
        [Display(Name = "Odaberi aktivnost")]
        public int AktivnostId { get; set; }



        [Display(Name = "Prisutnost")]
        public IsPrisutan IsPrisutan { get; set; }

    }
    public enum IsPrisutan
    {
        Da,
        Ne
    }
}