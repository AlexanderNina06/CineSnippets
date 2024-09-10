using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Productora 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Serie> Series { get; set; }
    }
}
