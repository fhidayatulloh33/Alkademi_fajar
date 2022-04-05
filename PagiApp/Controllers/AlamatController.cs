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
public class AlamatController : Controller
{
    private readonly IAlamatService _alamatService;
    private readonly ILogger<AlamatController> _logger;

    public AlamatController(ILogger<AlamatController> logger, IAlamatService alamatService)
    {
        _logger = logger;
        _alamatService = alamatService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _alamatService.GetAll();

        var viewModels = new List<AlamatViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new AlamatViewModel{
                IdAlamat = dbResult[i].IdAlamat,
                Provinsi = dbResult[i].Provinsi,
                Kabupaten = dbResult[i].Kabupaten,
                Kecamatan = dbResult[i].Kecamatan,
                Desa = dbResult[i].Desa,
                RtRw = dbResult[i].RtRw,
                KodePos = dbResult[i].KodePos,
                Deskripsi = dbResult[i].Deskripsi,
            });
        }

        return View(viewModels);
    }

    public IActionResult Create() {
        
        return View(new AlamatViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AlamatViewModel request) {
        if(!ModelState.IsValid){
            return View(request);
        }
        try{
            await _alamatService.Add(
                new Datas.Entities.Alamat{
                    IdAlamat = request.IdAlamat,
                    IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
                    Provinsi = request.Provinsi,
                    Kabupaten = request.Kabupaten,
                    Kecamatan = request.Kecamatan,
                    Desa = request.Desa,
                    RtRw = request.RtRw,
                    KodePos = request.KodePos,
                    Deskripsi = request.Deskripsi,
                }
            );

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
            var result = await _alamatService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new AlamatViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, AlamatViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        
        if(!ModelState.IsValid){
            return View(request);
        }

        try{
            await _alamatService.Update(request.ConvertToDbModel());

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
            var result = await _alamatService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new AlamatViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id, AlamatViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        try{
            await _alamatService.Delete(id.Value);

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