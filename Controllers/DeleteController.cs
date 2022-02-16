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
    public class DeleteController : Controller
    {
        private readonly ILogger<DeleteController> _logger;
        private readonly IServiceProvider _provider;

        public DeleteController(ILogger<DeleteController> logger,
    IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }
        [HttpGet]
        public IActionResult DeletePerson(int id)
        {
            if (id == 0)
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

        [HttpPost, ActionName("DeletePerson")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var context = GetContext();
            var person = await context.People.FindAsync(id);
            context.People.Remove(person);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private DataContext GetContext() => (DataContext)_provider.GetService(typeof(DataContext));


    }
}
