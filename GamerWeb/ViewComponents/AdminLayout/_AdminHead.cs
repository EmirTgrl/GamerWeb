using Microsoft.AspNetCore.Mvc;
namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminHead : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
