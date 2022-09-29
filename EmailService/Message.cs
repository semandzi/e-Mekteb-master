using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EmailService
{
    public class Message
    {
        [Required(ErrorMessage ="Ime je obavezno polje.")]
        [MaxLength(50)]
        public string Name { get; set; }
        
        

        [Required(ErrorMessage ="Mobitel je obavezno polje.")]
        [Phone]
        public string Mob { get; set; }


        [Required(ErrorMessage ="Email je obavezno polje.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Naslov je obavezno polje.")]
        [MaxLength(50)]
        public string Subject { get; set; }

        [Required(ErrorMessage ="Poruka je obavezno polje.")]
        public string Content { get; set; }


        
        
       
    }
}
