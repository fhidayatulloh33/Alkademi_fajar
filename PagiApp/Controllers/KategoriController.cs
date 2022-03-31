using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagiApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PagiApp.Controllers;

[Authorize]
public class KategoriController : Controller
{
    private readonly IKategoriService _kategoriService;
    private readonly ILogger<KategoriController> _logger;

    public KategoriController(ILogger<KategoriController> logger, IKategoriService kategoriService)
    {
        _logger = logger;
        _kategoriService = kategoriService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _kategoriService.GetAll();

        var viewModels = new List<KategoriViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new KategoriViewModel{
                IdKategori = dbResult[i].IdKategori,
                Nama = dbResult[i].Nama,
                Deskripsi = dbResult[i].Deskripsi,
            });
        }

        return View(viewModels);
    }

    public IActionResult Create() {
        return View(new KategoriViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(KategoriViewModel request) {
        if(!ModelState.IsValid){
            return View(request);
        }
        try{
            await _kategoriService.Add(request.ConvertToDbModel());

            return Redirect(nameof(Index));   
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int? id) 
    {

            if (id == null)
            {
                return NotFound();
            }
            var result = await _kategoriService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new KategoriViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, KategoriViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        
        if(!ModelState.IsValid){
            return View(request);
        }

        try{
            await _kategoriService.Update(request.ConvertToDbModel());

            return RedirectToAction(nameof(Index));   

        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id) 
    {

            if (id == null)
            {
                return NotFound();
            }
            var result = await _kategoriService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new KategoriViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id, KategoriViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        try{
            await _kategoriService.Delete(id.Value);

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