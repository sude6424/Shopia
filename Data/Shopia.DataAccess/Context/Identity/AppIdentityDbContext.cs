using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopia.DataAccess.Context.Identity;

public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }

    // Parametresiz constructor ekle
    public AppIdentityDbContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ShopiaDb;Integrated Security=True;TrustServerCertificate=True;");
        }
    }

}
