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
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);
app.Run();
