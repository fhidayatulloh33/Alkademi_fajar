using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagiApp.Interfaces;

namespace PagiApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProdukService _produkService;

    public HomeController(ILogger<HomeController> logger,
    IProdukService productService)
    {
        _logger = logger;
        _produkService = productService;

    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _produkService.GetAll();

        var viewModels = new List<ProdukViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new ProdukViewModel{
                Nama = dbResult[i].Nama,
                Harga = dbResult[i].Harga,
                Gambar = dbResult[i].Gambar,
            });
        }

        return View(viewModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
