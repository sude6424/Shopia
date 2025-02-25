using Shopia.Application.Interface;
using Shopia.Application.Interface.ICartItemsRepository;
using Shopia.Application.Interface.IProductsRepository;
using Shopia.Application.Services.CartItemServices;
using Shopia.Application.Services.CartServices;
using Shopia.Application.Services.CategoryServices;
using Shopia.Application.Services.CustomerServices;
using Shopia.Application.Services.OrderItemServices;
using Shopia.Application.Services.OrderServices;
using Shopia.Application.Services.ProductServices;
using Shopia.DataAccess.Context;
using Shopia.DataAccess.Repositories;
using Shopia.DataAccess.Repositories.CartItemsRepository;
using Shopia.DataAccess.Repositories.ProductsRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<ICartItemServices, CartItemServices>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();












// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
