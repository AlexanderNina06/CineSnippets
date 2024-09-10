 using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SerieViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImagenPortada { get; set; }
        public string EnlaceVideoYouTube { get; set; }
        public string ProductoraNombre { get; set; }
        public ICollection<string> Generos { get; set; }
        public string SegundoGeneroNombre { get; set; } 


    }
}
