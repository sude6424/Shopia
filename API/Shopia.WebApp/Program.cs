using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopia.Application.Interface;
using Shopia.Application.Interface.ICartItemsRepository;
using Shopia.Application.Interface.ICartRepository;
using Shopia.Application.Interface.IOrderRepository;
using Shopia.Application.Interface.IProductsRepository;
using Shopia.Application.Services.AccountServices;
using Shopia.Application.Services.CartItemServices;
using Shopia.Application.Services.CartServices;
using Shopia.Application.Services.CustomerServices;
using Shopia.Application.Services.HelpServices;
using Shopia.Application.Services.OrderServices;
using Shopia.Application.Services.ProductServices;
using Shopia.Application.Services.SubscriberServices;
using Shopia.DataAccess.Context;
using Shopia.DataAccess.Context.Identity;
using Shopia.DataAccess.Repositories;
using Shopia.DataAccess.Repositories.CartItemsRepository;
using Shopia.DataAccess.Repositories.CartsRepository;
using Shopia.DataAccess.Repositories.OrdersRepository;
using Shopia.DataAccess.Repositories.ProductsRepository;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));


// ✔ Diğer Servisler
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<ICartItemServices, CartItemServices>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<ISubscriberServices, SubscriberServices>();
builder.Services.AddScoped<IHelpServices, HelpServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

// ✔ Identity Kullanıcı Yönetimi
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
    .AddRoles<AppIdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

// ✔ Authentication (Kimlik Doğrulama) ve Authorization (Yetkilendirme)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

// ✔ Authorization ekle
builder.Services.AddAuthorization();  // Bu satırı ekleyin

// ✔ Controllers ve Razor Pages ekle (TempData için gerekli)
builder.Services.AddControllersWithViews();  // Bu satırı ekleyin

var app = builder.Build();

// ✔ Pipeline Yapılandırması
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✔ Kimlik Doğrulama ve Yetkilendirme
app.UseAuthentication();
app.UseAuthorization();  // Bu satırı da ekleyin

// ✔ Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
