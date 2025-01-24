using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.ViewComponents.Default_Index
{
    public class _DefaultFooterTop : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
