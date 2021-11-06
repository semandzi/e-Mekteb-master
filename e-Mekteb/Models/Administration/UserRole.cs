using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models.Administration
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string ImeIPrezime { get; set; }
        public bool IsSelected { get; set; }
    }
}
