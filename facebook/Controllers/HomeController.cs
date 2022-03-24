using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using facebook.Models;
using facebook.Services; 

namespace facebook.Controllers;

public class HomeController : Controller
{
    List<LoginViewModel> _listLogin = new List<LoginViewModel>();
    private readonly ILoginService _loginService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _loginService = loginService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(_loginService.GetLogin());
    }

    // GET: LoginController/Create
    public ActionResult Create()
    {
        return View(new LoginViewModel("Username ", "Password"));
    }

    // POST: LoginController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(LoginViewModel collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(collection);
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
