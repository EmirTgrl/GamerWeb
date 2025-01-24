using Microsoft.AspNetCore.Mvc;
namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminLogo : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
