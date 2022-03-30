using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagiApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagiApp.Helpers;

namespace PagiApp.Controllers;

public class ProdukController : Controller
{
    private readonly IProdukService _produkService;
    private readonly IKategoriService _kategoriService;
    private readonly IWebHostEnvironment _iWebHost;
    private readonly ILogger<ProdukController> _logger;

    public ProdukController(ILogger<ProdukController> logger, 
    IProdukService productService, 
    IKategoriService kategoriService,
    IWebHostEnvironment iwebHost)
    {
        _logger = logger;
        _produkService = productService;
        _kategoriService = kategoriService;
        _iWebHost = iwebHost;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _produkService.GetAll();

        var viewModels = new List<ProdukViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new ProdukViewModel{
                IdProduct = dbResult[i].IdProduct,
                Nama = dbResult[i].Nama,
                Deskripsi = dbResult[i].Deskripsi,
                Gambar = dbResult[i].Gambar,
                Harga = dbResult[i].Harga,
                Stock = dbResult[i].Stock,
                Kategories = dbResult[i].ProductKategoris.Select(x => new KategoriViewModel {
                    IdKategori = x.IdKategori,
                    Nama = x.IdKategoriNavigation.Nama,
                    Icon = x.IdKategoriNavigation.Icon
                }).ToList()
            });
        }

        return View(viewModels);
    }
    //Transfer data list of kategori ke view dimasukan dalam selectlistitem
    private async Task SetKategoriDataSource()
    {
        var kategoriViewModels = await _kategoriService.GetAll();

        ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
        {
            Value = x.IdKategori.ToString(),
            Text = x.Nama,
            Selected = false
        }).ToList();
    }
    public async Task<IActionResult> Create() {

        await SetKategoriDataSource();
        return View(new ProdukViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProdukViewModel request) {
        if(!ModelState.IsValid){
            await SetKategoriDataSource();
            return View(request);
        }
        if(request == null){
            await SetKategoriDataSource();
            return View(request);
        }

        try{
            string fileName = string.Empty;

            if(request.GambarFile != null) 
            {
                fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";

                using(var streamWriter = System.IO.File.Create(filePathName)){
                    //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                    //using extension to convert stream to bytes
                    await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                }
            }

            var product = request.ConvertToDbModel();
            product.Gambar = $"images/{fileName}";

            //Insert to ProdukKategori table
            for (int i = 0; i < request.KategoriId.Length; i++)
            { 
                product.ProductKategoris.Add(new Datas.Entities.ProductKategori 
                {
                    IdKategori = request.KategoriId[i],
                    IdProduct = product.IdProduct
                });   
            }

            await _produkService.Add(product);

            return Redirect(nameof(Index));
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }


        await SetKategoriDataSource();
        return View(request);
    }

    public async Task<IActionResult> Edit(int? id) 
    {

            if (id == null)
            {
                return NotFound();
            }
            var result = await _produkService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new ProdukViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, ProdukViewModel request) {
        if(!ModelState.IsValid){
            return View(request);
        }

        try{
            string fileName = string.Empty;

            if(request.GambarFile != null) 
            {
                fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";

                using(var streamWriter = System.IO.File.Create(filePathName)){
                    await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                }
            }

            var product = request.ConvertToDbModel();
            product.Gambar = $"images/{fileName}";

            //Insert to ProdukKategori table
            for (int i = 0; i < request.KategoriId.Length; i++)
            { 
                product.ProductKategoris.Add(new Datas.Entities.ProductKategori 
                {
                    IdKategori = request.KategoriId[i],
                    IdProduct = product.IdProduct
                });   
            }

            await _produkService.Update(request.ConvertToDbModel());

            return RedirectToAction(nameof(Index));   

        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }
        await SetKategoriDataSource();
        return View(request);
    }

    public async Task<IActionResult> Delete(int? id) 
    {

            if (id == null)
            {
                return NotFound();
            }
            var result = await _produkService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new ProdukViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id, ProdukViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        try{
            await _produkService.Delete(id.Value);

            return RedirectToAction(nameof(Index));   

        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}