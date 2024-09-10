using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class saveSerieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Debe ingresar el url de la imagen.")]
        public string ImagenPortada { get; set; }

        [Required(ErrorMessage = "Debe ingresar el url del video.")]
        public string EnlaceVideoYouTube { get; set; }

        [Required(ErrorMessage = "Debe ingresar la productora.")]
        public int ProductoraId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar al menos un género.")]
        public List<int> GeneroIds { get; set; }
        public int? SegundoGeneroId { get; set; }



    }
}
