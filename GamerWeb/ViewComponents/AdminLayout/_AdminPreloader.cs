﻿using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.ViewComponents.AdminLayout
{
    public class _AdminPreloader : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
