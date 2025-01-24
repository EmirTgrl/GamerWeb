using Microsoft.AspNetCore.Identity;

namespace GamerWeb.Entity.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
    }
}
