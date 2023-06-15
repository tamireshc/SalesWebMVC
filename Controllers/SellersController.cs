using Microsoft.AspNetCore.Mvc;

namespace salesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
