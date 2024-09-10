using Application.Service;
using Application.ViewModels;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCcrud.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _serieService;

        public SerieController(ApplicationContext dbContext)
        {
            _serieService = new(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            var seriesViewModels = await _serieService.GetAllViewModels();
            return View(seriesViewModels);
        }
        public IActionResult Create()
        {

            ViewBag.Productoras = _serieService.GetAllProductoras(); 
            ViewBag.Generos = _serieService.GetAllGeneros(); 
            return View("SaveSerie", new saveSerieViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(saveSerieViewModel vm)
        {
                if (!ModelState.IsValid)
                {
                ViewBag.Productoras = _serieService.GetAllProductoras();
                ViewBag.Generos = _serieService.GetAllGeneros();
                return View("SaveSerie", vm);
                }

            if (vm.SegundoGeneroId.HasValue && vm.SegundoGeneroId.Value > 0)
            {
                vm.GeneroIds.Add(vm.SegundoGeneroId.Value);
            }

            Serie serie = new Serie
            {
                Nombre = vm.Nombre,
                ImagenPortada = vm.ImagenPortada,
                EnlaceVideoYouTube = vm.EnlaceVideoYouTube,
                ProductoraId = vm.ProductoraId
            };

            await _serieService.Add(serie, vm.GeneroIds);
            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _serieService.GetByIdSaveViewModel(id);
            ViewBag.Productoras = _serieService.GetAllProductoras();

            ViewBag.Generos = _serieService.GetAllGeneros();


            return View("SaveSerie", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(saveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveSerie", vm);
            }

            await _serieService.Update(vm);
            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _serieService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _serieService.Delete(id);

            return RedirectToRoute(new { controller = "Serie", action = "Index" });
        }

    }
}
