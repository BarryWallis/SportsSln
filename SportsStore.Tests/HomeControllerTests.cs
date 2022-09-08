using Moq;

using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests;
public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        Product[] expected = new Product[]
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" },
            new Product { ProductId = 3, Name = "P3" },
            new Product { ProductId = 4, Name = "P4" },
            new Product { ProductId = 5, Name = "P5" },
        };
        Mock<IStoreRepository> mock = new();
        _ = mock.Setup(sr => sr.Products).Returns(expected.AsQueryable());
        HomeController homeController = new(mock.Object);

        ProductsListViewModel actual
            = homeController.Index(null)?.ViewData.Model as ProductsListViewModel ?? new();

        Assert.Equal(expected.Take(4), actual.Products.ToArray());
    }

    [Fact]
    public void CanPaginate()
    {
        Product[] products = new Product[]
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" },
            new Product { ProductId = 3, Name = "P3" },
            new Product { ProductId = 4, Name = "P4" },
            new Product { ProductId = 5, Name = "P5" },
        };
        const int PageSize = 3;
        Mock<IStoreRepository> mock = new();
        _ = mock.Setup(sr => sr.Products).Returns(products.AsQueryable());
        HomeController homeController = new(mock.Object)
        {
            PageSize = PageSize
        };

        ProductsListViewModel actual
            = homeController.Index(null, 2)?.ViewData.Model as ProductsListViewModel ?? new();

        Product[] expected = products.Skip(PageSize).ToArray();
        Assert.Equal(expected, actual.Products.ToArray());
    }

    [Fact]
    public void CanSendPaginationViewModel()
    {
        Product[] products = new Product[]
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" },
            new Product { ProductId = 3, Name = "P3" },
            new Product { ProductId = 4, Name = "P4" },
            new Product { ProductId = 5, Name = "P5" },
        };
        Mock<IStoreRepository> mock = new();
        _ = mock.Setup(sr => sr.Products).Returns(products.AsQueryable());
        HomeController homeController = new(mock.Object)
        {
            PageSize = 3
        };

        ProductsListViewModel actual = homeController.Index(null, 2)?.ViewData.Model as ProductsListViewModel ?? new();

        PagingInfo pagingInfo = actual.PagingInfo;
        Assert.Equal(2, pagingInfo.CurrentPage);
        Assert.Equal(3, pagingInfo.ItemsPerPage);
        Assert.Equal(5, pagingInfo.TotalItems);
        Assert.Equal(2, pagingInfo.TotalPages);
    }

    [Fact]
    public void CanFilterProducts()
    {
        Mock<IStoreRepository> mock = new();
        _ = mock.Setup(sr => sr.Products).Returns((new Product[]
        {
            new Product{ProductId=1, Name="P1", Category="Cat1"},
            new Product{ProductId=2, Name="P2", Category="Cat2"},
            new Product{ProductId=3, Name="P3", Category="Cat1"},
            new Product{ProductId=4, Name="P4", Category="Cat2"},
            new Product{ProductId=5, Name="P5", Category="Cat3"},
        }).AsQueryable());
        HomeController homeController = new(mock.Object)
        {
            PageSize = 3
        };

        Product[] actual = (homeController.Index("Cat2", 1)?.ViewData.Model as ProductsListViewModel
                            ?? new()).Products.ToArray();

        Assert.Equal(2, actual.Length);
        Assert.True(actual[0].Name == "P2" && actual[0].Category == "Cat2");
        Assert.True(actual[1].Name == "P4" && actual[1].Category == "Cat2");
    }
}
