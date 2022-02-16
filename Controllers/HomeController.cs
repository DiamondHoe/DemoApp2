using Demo2.Data;
using Demo2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _provider;

        public HomeController(ILogger<HomeController> logger,
            IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = GetContext().People.ToList();
            return View(values);
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private DataContext GetContext() => (DataContext)_provider.GetService(typeof(DataContext));

    }
}
