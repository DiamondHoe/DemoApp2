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
    public class AddController : Controller
    {
        private readonly ILogger<AddController> _logger;
        private readonly IServiceProvider _provider;

        public AddController(ILogger<AddController> logger,
            IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person _newPerson)
        {
            var _Person = new Person
            {
                FirstName = _newPerson.FirstName,
                LastName = _newPerson.LastName
            };
            if ((_newPerson.FirstName != null) && (_newPerson.LastName != null))
            {
                var context = GetContext();
                context.People.Add(_Person);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(_Person);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private DataContext GetContext() => (DataContext)_provider.GetService(typeof(DataContext));

    }
}
