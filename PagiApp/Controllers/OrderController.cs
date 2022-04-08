using System.Globalization;
using System.Security.Principal;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System;
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
using PagiApp.Datas;
using PagiApp.Datas.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PagiApp.Controllers;

[Authorize]
public class OrderController : BaseController
{
    private readonly ILogger<OrderController> _logger;
    private readonly IKeranjangService _keranjangService;
    private readonly IOrderService _orderService;
    private readonly IStatusService _statusService;
    private readonly IWebHostEnvironment _iWebHost;

    public OrderController(ILogger<OrderController> logger, 
    IKeranjangService keranjangService,
    IOrderService orderService,
    IStatusService statusService,
    IWebHostEnvironment iWebHost)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _orderService = orderService;
        _statusService = statusService;
        _iWebHost = iWebHost;
    }

    public override void OnActionExecuting(ActionExecutingContext context){
        
        base.OnActionExecuting(context);
    }

    [Authorize(Roles = AppConstant.ADMIN)]
    public async Task<IActionResult> Index(int? page, int? pageCount)
    {
        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.Getv2(tuplePagination.Item1, tuplePagination.Item2, null, null);

        await SetStatusListAsSelectListItem();
        ViewBag.FilterDate = null;

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromQuery]int? page, [FromQuery]int? pageCount, int? status, DateTime? date)
    {
        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.Getv2(tuplePagination.Item1, tuplePagination.Item2, status, date);
    
        await SetStatusListAsSelectListItem(status);
        if(date != null)
        {
            ViewBag.FilterDate = date.Value.ToString("MM/dd/yyyy");
        }
        return View(result);
    }

    private async Task SetStatusListAsSelectListItem(int? status = null){
        var statusList = await _statusService.Get();

        if(statusList == null || !statusList.Any()){
            ViewBag.StatusList = new List<SelectListItem>();
        }else{
            ViewBag.StatusList = statusList.Select(x=> new SelectListItem {
                Value = x.IdStatus.ToString(),
                Text = x.Nama,
                Selected = status != null && status.Value == x.IdStatus
            }).ToList();
        }
    }

    public async Task<IActionResult> Detail(int? id)
    {
       if (id == null)
        {
            return BadRequest();
        }

        var result = await _orderService.GetDetail(id.Value);

        return View(result);
    }


    [Authorize(Roles = AppConstant.CUSTOMER)]
    public async Task<IActionResult> MyOrder()
    {
        var result = await _orderService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
    
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel? request)
    {
        if(request == null)
        {
            return BadRequest();
        }

        if(request.Alamat == 0)
        {
            return BadRequest();
        }

        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idCustomer);

        if(result == null || !result.Any())
        {
            return BadRequest();
        }

        foreach (var item in result)
        {
            int keranjangId = request.IdProduct.FirstOrDefault(x=> item.IdKeranjang == x);
            
            if(keranjangId < 1)
            {
                continue;
            }
            int jumlahBarangBaru = request.Qty[Array.IndexOf(request.IdProduct, keranjangId)];

            item.JmlBarang = jumlahBarangBaru;
            item.Subtotol = item.HargaBarang * jumlahBarangBaru;
        }

        var newOrder = new Order();

        newOrder.IdCustomer = idCustomer;
        newOrder.JmlBayar =  Convert.ToInt32(result.Sum(x=>x.Subtotol));
        newOrder.Note = string.Empty;
        newOrder.Status = 1;
        newOrder.IdAlamat = request.Alamat;
        newOrder.TglTransaksi = DateTime.Now;
        newOrder.Detailorders = new List<Detailorder>();

        foreach(var item in result)
        {
            newOrder.Detailorders.Add(new Detailorder
            {
                IdOrder = newOrder.IdOrder,
                Harga = item.HargaBarang,
                JmlBarang = Convert.ToInt32(item.JmlBarang),
                SubTotal = item.Subtotol,
                IdProduct = item.IdProduct
            });
        }
        
        await _orderService.Checkout(newOrder);

        await _keranjangService.Clear(idCustomer);

        return RedirectToAction(nameof(CheckoutBerhasil));
    }

    
    public async Task<IActionResult> MyOrderDetails(int? id)
    {
       var result = await _orderService.GetDetail(id.Value, HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
    
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Bayar(BayarViewModel request)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        //Simpan file
        string fileName = string.Empty;
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

        if (request.FileBuktiBayar != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.FileBuktiBayar?.FileName}";

            string filePathName = _iWebHost.WebRootPath + "\\images\\" + fileName;

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                await streamWriter.WriteAsync(request.FileBuktiBayar.OpenReadStream().ToBytes());
            }
        }

        Pembayaran newBayar = new Pembayaran()
        {
            FileBuktiPembayaran = "images/" + fileName,
            IdCustomer = idCustomer,
            IdOrder = request.IdOrder,
            TglBayar = request.TglBayar,
            Tujuan = request.IdTujuan,
            JumlahBayar = request.JmlBayar,
            MetodeBayar = request.MetodeBayar,
            Pajak = 11000
        };

        await _orderService.Bayar(newBayar);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DIBAYAR);

        return RedirectToAction(nameof(MyOrderDetails), new
        {
            id = request.IdOrder
        });
    }

    [HttpPost]
    public async Task<IActionResult> Konfirmasi(int? IdOrder) {
        if(IdOrder == null)
        {
            return BadRequest();
        }
        
        //SOLID Principle
        if(!await _orderService.SudahDibayar(IdOrder.Value))
        {
            return BadRequest();   
        }

        await _orderService.UpdateStatus(IdOrder.Value, AppConstant.StatusOrder.DIPROSES);

        return RedirectToAction(nameof(Detail), new
        {
            id = IdOrder.Value
        });
    }

    public async Task<IActionResult> Kirim(KirimRequestViewModel request){
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        Pengiriman dataPengiriman = new Pengiriman
        {
            IdOrder = request.IdOrder,
            IdAlamat = request.IdAlamat,
            Kurir = request.Kurir,
            NoResi = request.NoResi,
            Ongkir = request.Ongkir,
            Status = string.Empty,
            Keterangan = request.Keterangan
        };

        await _orderService.Kirim(dataPengiriman);

        await _orderService.UpdateStatus(dataPengiriman.IdOrder, AppConstant.StatusOrder.DIKIRIM);

        return RedirectToAction(nameof(Detail), new {
            id = request.IdOrder
        });
    }

    public async Task<IActionResult> Review(UlasanRequestViewModel request)
    {
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

        string fileName = string.Empty;
        
        if (request.Gambar != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.Gambar?.FileName}";

            string filePathName = _iWebHost.WebRootPath + "\\images\\" + fileName;

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                //using extension to convert stream to bytes
                await streamWriter.WriteAsync(request.Gambar.OpenReadStream().ToBytes());
            }
        }

        Ulasan ulasan = new Ulasan{
            IdOrder = request.IdOrder,
            IdCustomer = idCustomer,
            Komentar = request.Komentar,
            Rating = request.Rating,
            Gambar = string.IsNullOrEmpty(fileName) ? string.Empty : "images/" + fileName
        };


        await _orderService.Ulas(ulasan);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DITERIMA);

        return RedirectToAction(nameof(MyOrderDetails), new {
            id = request.IdOrder
        });
    }


    public IActionResult CheckoutBerhasil(){
        return View();
    }
}

