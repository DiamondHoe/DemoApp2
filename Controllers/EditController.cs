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
    public class EditController : Controller
    {
        private readonly ILogger<EditController> _logger;
        private readonly IServiceProvider _provider;

        public EditController(ILogger<EditController> logger,
    IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet]
        public IActionResult EditPerson(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var person = GetContext().People.FirstOrDefault(m => m.Id == id);
            if (person == null)
            {
                return View();
            }

            return View(person);
        }
        [HttpPost]
        public IActionResult EditPerson(int id, [Bind("Id, FirstName, LastName")] Person person)
        {
            if (id != person.Id)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                var context = GetContext();
                context.Update(person);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new {message = "Pomyślnie edytowano osobę" });
            }
            return View(person);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private DataContext GetContext() => (DataContext)_provider.GetService(typeof(DataContext));

    }
}
