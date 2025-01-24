using GamerWeb.Areas.Admin.Models;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserWithRolesDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserWithRolesDto
                {
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = string.Join(", ", roles) // Kullanıcının rollerini virgülle ayırarak yazdırıyoruz
                });
            }

            return View(userList);
        }
    }
}
