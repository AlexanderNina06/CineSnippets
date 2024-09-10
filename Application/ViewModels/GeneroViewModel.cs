using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class GeneroViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre del genero")]

        public string Nombre { get; set; }
    }
}
