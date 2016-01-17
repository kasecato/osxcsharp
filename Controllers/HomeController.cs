using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StarWars.Models;
using Microsoft.Extensions.DependencyInjection;
using StarWars.ViewModels;

namespace StarWars.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var serviceScope = this.Resolver
                        .GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<StarWarsContext>();

                var dirEp4 = new Director()
                {
                    DirectorId = 1,
                    Episode = 4,
                    Name = "George Walton Lucas, Jr.",
                    Born = DateTime.Parse("May 14, 1944")
                };

                var dirEp7 = new Director()
                {
                    DirectorId = 4,
                    Episode = 7,
                    Name = "J. J. Abrams",
                    Born = DateTime.Parse("June 27, 1966")
                };

                // DELETE
                foreach (var row in dbContext.Directors)
                {
                    dbContext.Directors.Remove(row);
                }
                dbContext.SaveChanges();

                // INSERT
                dbContext.Directors.AddRange(dirEp4, dirEp7);
                dbContext.SaveChanges();

                // SELECT
                var dir = dbContext.Directors.ToList();

                return View(dir);
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
