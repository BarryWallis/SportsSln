using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using SportsStore.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
Debug.Assert(builder.Configuration["ConnectionStrings:SportsStoreConnection"] is not null);
builder.Services.AddDbContext<StoreDbContext>(o
    => o.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]!));
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
WebApplication app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    "catpage",
    "{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" });
app.MapControllerRoute(
    "page",
    "Page{productPage:int}",
    new { Conroller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute(
    "category",
    "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute(
    "pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);
app.Run();
