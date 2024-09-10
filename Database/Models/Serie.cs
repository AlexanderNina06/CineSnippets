using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImagenPortada { get; set; }
        public string EnlaceVideoYouTube { get; set; }
        public int ProductoraId { get; set; }

        //Navigation property
        public Productora Productora { get; set; }

        public ICollection<SerieGenero> SerieGeneros { get; set; }
    }
}
