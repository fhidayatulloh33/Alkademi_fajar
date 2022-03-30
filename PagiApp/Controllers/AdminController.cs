using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagiApp.Models;
using PagiApp.ViewModels;
using PagiApp.Datas;
using PagiApp.Datas.Entities;

namespace PagiApp.Controllers
{
    public class AdminController : Controller
    {

        public async Task<ActionResult> Index()
        {
            return View();
        }

    }
}
