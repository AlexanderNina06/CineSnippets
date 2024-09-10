using Microsoft.AspNetCore.Mvc;

namespace MVCcrud.Controllers
{
    public class LandingPageController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Layout = null;
            return View();
        }
    }
}
