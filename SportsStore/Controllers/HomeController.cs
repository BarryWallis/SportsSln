using Microsoft.AspNetCore.Mvc;

using SportsStore.Models;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _storeRepository;

    public HomeController(IStoreRepository storeRepository) => _storeRepository = storeRepository;

    public IActionResult Index() => View(_storeRepository.Products);
}
