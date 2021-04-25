using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment4.DataAccess;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class DeleteController : Controller
    {
        private readonly Assignment4DbContext dbContext;

        public DeleteController(Assignment4DbContext context)
        {
            dbContext = context;
        }

        IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public  IActionResult Index(string county, string population, int value)
        {
            Demographic DelRecord = dbContext.Demographics
                        .Where(d => d.county.CountyName == county & d.population.PopTypeName == population)
                        .First();
            
                DeleteRecord DelRecord1 = new DeleteRecord();
                DelRecord1.county = county;
                DelRecord1.population = population;
                DelRecord1.Value = value;
                return View(DelRecord1); 
        }

        [HttpPost]
        public async Task<IActionResult> DelConfirm(string county, string population, int value, string confirmation)
        {
            Demographic DelRecord = dbContext.Demographics
                       .Where(d => d.county.CountyName == county & d.population.PopTypeName == population)
                       .First();
            if (confirmation == "Yes")
            {
                dbContext.Demographics.Remove(DelRecord);
                await dbContext.SaveChangesAsync();
                return View(DelRecord);
            }
            else
            {
                return View();
            }
            
        }
    }
}