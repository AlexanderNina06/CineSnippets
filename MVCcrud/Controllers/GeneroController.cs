using Application.Service;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace MVCcrud.Controllers
{
    public class GeneroController : Controller
    {
        private readonly GeneroService _generoService;

        public GeneroController(ApplicationContext dbContext)
        {
            _generoService = new GeneroService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            var generoViewModels = await _generoService.GetAllViewModels();
            return View(generoViewModels);
        }

        public IActionResult Create()
        {
            return View("SaveGenero", new GeneroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenero", vm);
            }

            await _generoService.Add(vm);
            return RedirectToRoute(new { controller = "Genero", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveGenero", await _generoService.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GeneroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenero", vm);
            }

            await _generoService.Update(vm);
            return RedirectToRoute(new { controller = "Genero", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _generoService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _generoService.Delete(id);

            return RedirectToRoute(new { controller = "Genero", action = "Index" });
        }
    }
}
