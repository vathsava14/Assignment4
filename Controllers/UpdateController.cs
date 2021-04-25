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
    public class UpdateController : Controller
    {
        private readonly Assignment4DbContext dbContext;

        public UpdateController(Assignment4DbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string county, string population, int? value, string? valueStr)
        {
            Demographic modRecord = dbContext.Demographics
                        .Where(d => d.county.CountyName == county & d.population.PopTypeName == population)
                        .First();
            if (modRecord.Value == value)
            {
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.County = county;
                updRecord.Population = population;
                updRecord.Value = (int)value;
                updRecord.origValue = value;
                return View(updRecord);
            }
            else
            {
                modRecord.Value = Convert.ToInt32(valueStr);
                dbContext.Demographics.Update(modRecord);
                await dbContext.SaveChangesAsync();
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.County = county;
                updRecord.Population = population;
                updRecord.Value = dbContext.Demographics
                        .Where(d => d.county.CountyName == county & d.population.PopTypeName == population)
                        .Select(v => v.Value)
                        .First();
                updRecord.origValue = value;
                return View(updRecord);
            }
        }
    }
}