using Application.Service;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace MVCcrud.Controllers
{
    public class ProductoraController : Controller
    {
        private readonly ProductoraService _productoraService;

        public ProductoraController(ApplicationContext dbContext)
        {
            _productoraService = new ProductoraService(dbContext);
        }
        public async Task<IActionResult> Index()
        {
            var productorasViewModels = await _productoraService.GetAllViewModels();
            return View(productorasViewModels);
        }

        public IActionResult Create()
        {
            return View("SaveProductora", new ProductoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProductora", vm);
            }

            await _productoraService.Add(vm);
            return RedirectToRoute(new { controller = "Productora", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProductora", await _productoraService.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProductora", vm);
            }

            await _productoraService.Update(vm);
            return RedirectToRoute(new { controller = "Productora", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _productoraService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _productoraService.Delete(id);

            return RedirectToRoute(new { controller = "Productora", action = "Index" });
        }
    }
}
