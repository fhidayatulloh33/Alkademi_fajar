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
using PagiApp.Datas.Entities;

namespace PagiApp.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER)]
public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IKeranjangService _keranjangService;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IKeranjangService keranjangService,
    IOrderService orderService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _orderService = orderService;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (HttpContext.User == null || HttpContext.User.Identity == null)
        {
            ViewBag.IsLogged = false;
        }
        else
        {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
        }

        base.OnActionExecuted(context);
    }

    public async Task<IActionResult> Index()
    {

        var result = await _keranjangService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel? request)
    {
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idCustomer);

        if(result == null || !result.Any())
        {
            return BadRequest();
        }

        foreach (var item in result)
        {
            int keranjangId = request.Id.FirstOrDefault(x=> item.IdKeranjang == x);
            
            if(keranjangId < 1)
            {
                continue;
            }
            int jumlahBarangBaru = request.Qty[Array.IndexOf(request.Id, keranjangId)];

            item.JmlBarang = jumlahBarangBaru;
            item.Subtotol = item.HargaBarang * jumlahBarangBaru;
        }

        var newOrder = new Order();

        newOrder.IdCustomer = idCustomer;
        //newOrder.JmlBayar = result.Sum(x=>x.Subtotol);
        newOrder.Note = string.Empty;
        newOrder.Status = "Masuk";
        newOrder.IdAlamat = request.Alamat;
        newOrder.TglTransaksi = DateTime.Now;
        newOrder.Detailorders = new List<Detailorder>();

        foreach(var item in result)
        {
            newOrder.Detailorders.Add(new Detailorder
            {
                IdOrder = newOrder.IdOrder,
                Harga = item.HargaBarang,
                JmlBarang = item.JmlBarang,
                SubTotal = item.Subtotol,
                IdProduct = item.IdProduct
            });
        }
        
        await _orderService.Checkout(newOrder);

        await _keranjangService.Clear(idCustomer);

        return RedirectToAction(nameof(CheckoutBerhasil));
        //return View();
    }

    public IActionResult CheckoutBerhasil(){
        return View();
    }
}