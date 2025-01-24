using Microsoft.AspNetCore.Mvc;
namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminNavbar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
