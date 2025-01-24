using Microsoft.AspNetCore.Mvc;
namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminSidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
