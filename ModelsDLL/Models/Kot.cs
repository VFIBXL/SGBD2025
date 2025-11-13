using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDLL.Models
{
    public class Kot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Student? Resident { get; set; }
    }
}
