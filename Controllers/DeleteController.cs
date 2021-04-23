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
        private readonly Assignment4DbContext _context;

        public DeleteController(Assignment4DbContext context)
        {
            _context = context;
        }

        IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public  IActionResult Index(string sector, string source, int year, Decimal value)
        {
            AnnualEnergyConsumption DelRecord = _context.AnnualEnergyConsumption
                        .Where(t => t.sector.SectorName == sector & t.energysource.SourceName == source & t.Year == year)
                        .First();
            
                DeleteRecord DelRecord1 = new DeleteRecord();
                DelRecord1.Sector = sector;
                DelRecord1.Source = source;
                DelRecord1.Year = year;
                DelRecord1.Value = value;
                return View(DelRecord1); 
        }

        [HttpPost]
        public async Task<IActionResult> DelConfirm(string sector, string source, int year, Decimal value, string confirmation)
        {
            AnnualEnergyConsumption DelRecord = _context.AnnualEnergyConsumption
                       .Where(t => t.sector.SectorName == sector & t.energysource.SourceName == source & t.Year == year)
                       .First();
            if (confirmation == "Yes")
            {
                _context.AnnualEnergyConsumption.Remove(DelRecord);
                await _context.SaveChangesAsync();
                return View(DelRecord);
            }
            else
            {
                return View();
            }
            
        }
    }
}