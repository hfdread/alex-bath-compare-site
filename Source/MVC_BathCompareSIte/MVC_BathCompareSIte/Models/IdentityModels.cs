using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVC_BathCompareSIte.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ModelsContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ItemDbContext  : DbContext
    {
        public ItemDbContext()
            : base("ModelsContext")
        {

        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<ProdType> Types { get; set; }

        public DbSet<UserInventories> UserInventory { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Aspnetusers> AppUsers { get; set; }
        public DbSet<Aspnetroles> AppRoles { get; set; }
        public DbSet<Aspnetuserroles> AppUserRoleRelation { get; set; }
        public DbSet<AdminUsers> AdminUsers { get; set; }

    }
}