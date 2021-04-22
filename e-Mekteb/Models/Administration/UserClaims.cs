using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.Models.Administration
{
    public class UserClaims
    {
        public UserClaims()
        {
            Claims = new List<UserClaim>();

        }

        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
