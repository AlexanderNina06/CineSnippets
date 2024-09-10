using Application.Service;
using Database;
using Microsoft.AspNetCore.Mvc;
using MVCcrud.Models;
using System.Diagnostics;

namespace MVCcrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _serieService;

        public HomeController(ApplicationContext dbContext)
        {
            _serieService = new(dbContext);
        }

        public async Task<IActionResult> Index(string searchString, string productoraNombre, string generoNombre)
        {
            var seriesViewModels = _serieService.GetAllViewModels().Result; 

            ViewBag.Productoras = _serieService.GetAllProductoras(); 

            ViewBag.Generos = _serieService.GetAllGeneros(); 

            if (!string.IsNullOrEmpty(searchString))
            {
                seriesViewModels = seriesViewModels.Where(s => s.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(productoraNombre))
            {
                seriesViewModels = seriesViewModels.Where(s => s.ProductoraNombre.Contains(productoraNombre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(generoNombre))
            {
                seriesViewModels = seriesViewModels.Where(s => s.Generos.Contains(generoNombre, StringComparer.OrdinalIgnoreCase)).ToList();
            }


            return View(seriesViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var serieViewModel = await _serieService.GetByIdSaveViewModel(id);
            if (serieViewModel == null)
            {
                return NotFound();
            }
            return View(serieViewModel);
        }



    }
}
