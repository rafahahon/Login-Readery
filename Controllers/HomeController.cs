using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Login_Readery.Models;

namespace Login_Readery.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UsuarioNome") == null)
        {
            return RedirectToAction("Index", "Login");
        }

        // viewbag -> mochilinha que carrega as infomações para a view
        ViewBag.Usuario = HttpContext.Session.GetString("UsuarioNome");
        return View();
    }
}
