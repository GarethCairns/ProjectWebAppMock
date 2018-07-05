using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAppMock.Data;
using ProjectWebAppMock.Models;

namespace ProjectWebAppMock.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {

      //private ApplicationDbContext context = new ApplicationDbContext();

      return View();
    }

    public IActionResult About()
    {
      ////ViewData["Message"] = "Dashboard Area";
      // not being used, redirect in place
      //return View();
      return RedirectToAction("Index");
    }

    public IActionResult Contact()
    {
      //ViewData["Message"] = "Your contact page.";

      //return View();
      return RedirectToAction("Index");
    }




    //Alternate in place
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
  }
}
