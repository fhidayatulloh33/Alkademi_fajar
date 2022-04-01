using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using PagiApp.Interfaces;
using System.Security.Claims;
using PagiApp.Helpers;

namespace PagiApp.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER)]
public class KeranjangController : Controller
{
    private readonly ILogger<KeranjangController> _logger;
    private readonly IKeranjangService _keranjangService;

    public KeranjangController(ILogger<KeranjangController> logger, IKeranjangService keranjangService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(HttpContext.User == null || HttpContext.User.Identity == null){
            ViewBag.IsLogged = false;
        } else {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
        }

        base.OnActionExecuted(context);
    }

    public async Task<IActionResult> Index(){

        var result = await _keranjangService.Get(HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
        
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int? IdProduct)
    {
        if(IdProduct == null)
        {
            return BadRequest();
        }

        await _keranjangService.Add(new Datas.Entities.Keranjang
        {
            IdProduct = IdProduct.Value,
            JmlBarang = 1,
            IdCustomer = HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt()
        });

        return RedirectToAction(nameof(Index));
    }
}