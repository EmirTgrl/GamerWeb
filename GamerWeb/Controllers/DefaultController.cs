using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
