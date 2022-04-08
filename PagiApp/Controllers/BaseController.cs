using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PagiApp.Models;
using PagiApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using PagiApp.Interfaces;
using PagiApp.Helpers;

namespace PagiApp.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(HttpContext.User == null || HttpContext.User.Identity == null){
            ViewBag.IsLogged = false;
        } else {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.IsCustomer = HttpContext.User.IsInRole(AppConstant.CUSTOMER);
        }

        base.OnActionExecuted(context);
    }
}