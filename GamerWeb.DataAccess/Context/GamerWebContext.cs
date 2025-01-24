using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamerWeb.DataAccess.Context
{
    public class GamerWebContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=EMIR\\SQLEXPRESS;database=GamerWebDb;Integrated Security=true;TrustServerCertificate=true;");
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
