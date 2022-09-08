using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _storeRepository;

    public int PageSize { get; init; } = 4;

    public HomeController(IStoreRepository storeRepository) => _storeRepository = storeRepository;

    public ViewResult Index(string? category, int productPage = 1)
        => View(new ProductsListViewModel
        {
            Products = _storeRepository.Products
                                       .Where(p => category == null || p.Category == category)
                                       .OrderBy(p => p.ProductId)
                                       .Skip((productPage - 1) * PageSize)
                                       .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = _storeRepository.Products.Count()
            },
            CurrentCategory = category
        });
}
