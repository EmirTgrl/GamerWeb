using GamerWeb.Dto.Dtos.IdentityDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor");
                return View(registerDto);
            }

            var user = new AppUser
            {
                NameSurname = registerDto.NameSurname,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }

            // Kullanıcıyı oturum açmış olarak sisteme kaydetmek için PasswordSignInAsync kullanıyoruz.
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }

            return RedirectToAction("NewsList", "News", new { area = "Admin" });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated) // Kullanıcının giriş yapıp yapmadığını kontrol ediyoruz
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.GetUserAsync(User); // Şu anki kullanıcıyı al

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var updateProfileDto = new UpdateProfileDto
            {
                Username = user.UserName,
                Email = user.Email
            };

            return View(updateProfileDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Profile(UpdateProfileDto updateProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateProfileDto);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // E-posta değişikliği
            if (user.Email != updateProfileDto.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, updateProfileDto.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(updateProfileDto);
                }
            }

            // Kullanıcı adı değişikliği
            if (user.UserName != updateProfileDto.Username)
            {
                var setUsernameResult = await _userManager.SetUserNameAsync(user, updateProfileDto.Username);
                if (!setUsernameResult.Succeeded)
                {
                    foreach (var error in setUsernameResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(updateProfileDto);
                }
            }

            // Eğer yeni şifre girmek istiyorsa, şifre değişikliği yapılacak
            if (!string.IsNullOrEmpty(updateProfileDto.NewPassword) || !string.IsNullOrEmpty(updateProfileDto.ConfirmPassword))
            {
                // Mevcut şifre girilmediyse hata döndürüyoruz
                if (string.IsNullOrEmpty(updateProfileDto.CurrentPassword))
                {
                    ModelState.AddModelError("", "Mevcut şifre gereklidir.");
                    return View(updateProfileDto);
                }

                // Yeni şifre ile onay şifresi aynı mı kontrol et
                if (updateProfileDto.NewPassword != updateProfileDto.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Yeni şifreler eşleşmiyor.");
                    return View(updateProfileDto);
                }

                // Şifreyi değiştirme işlemi
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, updateProfileDto.CurrentPassword, updateProfileDto.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(updateProfileDto);
                }
            }

            // Eğer şifre değiştirilmek istenmiyorsa, sadece kullanıcı adı ve e-posta güncellenir
            TempData["SuccessMessage"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Profile");
        }

    }
}
