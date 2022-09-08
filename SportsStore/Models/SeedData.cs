using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public class SeedData
{
    public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
    {
        StoreDbContext storeDbContext = applicationBuilder.ApplicationServices.CreateScope()
                                                          .ServiceProvider
                                                          .GetRequiredService<StoreDbContext>();
        if (storeDbContext.Database.GetPendingMigrations().Any())
        {
            storeDbContext.Database.Migrate();
        }

        if (!storeDbContext.Products.Any())
        {
            StoreProducts(storeDbContext);
            _ = storeDbContext.SaveChanges();
        }
    }

    private static void StoreProducts(StoreDbContext storeDbContext) => storeDbContext.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275.00M
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashonable",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer",
                        Price = 34.95M
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000 seat stadium",
                        Category = "Soccer",
                        Price = 79_500.00M
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess",
                        Price = 16.00M
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29.95M
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75.00M
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded king",
                        Category = "Chess",
                        Price = 1200.00M
                    }
                    );
}
