using System.Security.Principal;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using PagiApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PagiApp.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger, IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }

    public IActionResult Index(){
        return View();
    }

    public async Task<IActionResult> LihatAdmin()
    {
        var dbResult = await _adminService.GetAll();

        var viewModels = new List<AdminViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new AdminViewModel{
                IdAdmin = dbResult[i].IdAdmin,
                Nama = dbResult[i].Nama,
                NoHp = dbResult[i].NoHp,
                Username = dbResult[i].Username,
                Password = dbResult[i].Password,
            });
        }

        return View(viewModels);
    }

    public IActionResult Create() {
        return View(new AdminViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminViewModel request) {
        if(!ModelState.IsValid){
            return View(request);
        }
        try{
            await _adminService.Add(request.ConvertToDbModel());

            return Redirect(nameof(LihatAdmin));   
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
            var result = await _adminService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new AdminViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, AdminViewModel request) {
        if(id == null) {
            return BadRequest();
        }

        if(!ModelState.IsValid){
            return View(request);
        }

        try{
            await _adminService.Update(request.ConvertToDbModel());

            return RedirectToAction(nameof(LihatAdmin));   

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
            var result = await _adminService.Get(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(new AdminViewModel(result));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id, AdminViewModel request) {
        if(id == null) {
            return BadRequest();
        }
        try{
            await _adminService.Delete(id.Value);

            return RedirectToAction(nameof(LihatAdmin));   

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