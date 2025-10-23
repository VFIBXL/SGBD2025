using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string Matricule { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; }
    }
}
