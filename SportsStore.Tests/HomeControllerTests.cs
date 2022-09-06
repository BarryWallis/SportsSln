using Microsoft.AspNetCore.Mvc;

using Moq;

using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;
public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        Product[] expected = new Product[]
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" }
        };
        Mock<IStoreRepository> mock = new();
        _ = mock.Setup(sr => sr.Products).Returns(expected.AsQueryable());
        HomeController homeController = new(mock.Object);

        IEnumerable<Product>? actual = (homeController.Index() as ViewResult)?.ViewData
                                                                              .Model as IEnumerable<Product>;

        Product[] products = actual?.ToArray() ?? Array.Empty<Product>();
        Assert.Equal(expected, products);
    }
}
