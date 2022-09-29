using e_Mekteb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_Mekteb.ViewModel
{
    public class StudentInfo
    {
        
        public bool IsSelected { get; set; }
        public string SchoolName { get; set; }
        public string  Grade { get; set; }        
        public string  Student { get; set; }
        public string  Id { get; set; }

    }
}