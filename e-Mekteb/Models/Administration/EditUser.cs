using e_Mekteb.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models.Administration
{
    public class EditUser
    {
        public EditUser()
        {
            Roles = new List<string>();
            Claims = new List<string>();
            Predmeti = new List<string>();
            PredmetiUcenika = new List<string>();
            Skole = new List<string>();
            RazrediUcenika = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Roles { get; set; }
        public List<string> Claims { get; set; }
        public List<string> Predmeti { get; set; }
        public List<string> PredmetiUcenika { get; set; }
        public List<string> Skole { get; set; }
        public List<string> RazrediUcenika { get; set; }
    }
}
