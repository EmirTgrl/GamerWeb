using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
