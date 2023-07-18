using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task32.MVC.Models;
using Task32.MVC.Models.Db;

namespace Task32.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggingRepository _repo;
        public HomeController(ILoggingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Logs()
        {
            var logs = await _repo.GetRequests();
            return View(logs);
        }
  
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
