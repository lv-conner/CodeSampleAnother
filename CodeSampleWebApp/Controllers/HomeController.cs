using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeSampleWebApp.Models;
using Microsoft.AspNetCore.Hosting;

namespace CodeSampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationLifetime _applicationLifetime;
        public HomeController(IApplicationLifetime applicationLifetime)
        {
            _applicationLifetime = applicationLifetime;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Stop()
        {
            _applicationLifetime.StopApplication();
            return Content("stop");
        }
    }
}
