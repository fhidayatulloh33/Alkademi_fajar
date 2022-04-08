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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PagiApp.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER)]
public class KeranjangController : BaseController
{
    private readonly ILogger<KeranjangController> _logger;
    private readonly IKeranjangService _keranjangService;
    private readonly IAccountService _accountService;

    public KeranjangController(ILogger<KeranjangController> logger, 
    IKeranjangService keranjangService,
    IAccountService accountService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _accountService = accountService;
    }

    public async Task<IActionResult> Index(){

        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idCustomer);

        var alamat = await _accountService.GetAlamat(idCustomer);

        ViewBag.AlamatList = alamat.Select(x=> new SelectListItem(x.Item2.ToString(), x.Item1.ToString())).ToList();
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