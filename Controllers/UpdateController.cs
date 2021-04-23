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
        private readonly Assignment4DbContext _context;

        public UpdateController(Assignment4DbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string sector, string source, int year, Decimal? value, string? valueStr)
        {
            AnnualEnergyConsumption modRecord = _context.AnnualEnergyConsumption
                        .Where(t => t.sector.SectorName == sector & t.energysource.SourceName == source & t.Year == year)
                        .First();
            if (modRecord.Value == value)
            {
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.Sector = sector;
                updRecord.Source = source;
                updRecord.Year = year;
                updRecord.Value = (Decimal)value;
                updRecord.origValue = value;
                return View(updRecord);
            }
            else
            {
                modRecord.Value = Convert.ToDecimal(valueStr);
                _context.AnnualEnergyConsumption.Update(modRecord);
                await _context.SaveChangesAsync();
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.Sector = sector;
                updRecord.Source = source;
                updRecord.Year = year;
                updRecord.Value = _context.AnnualEnergyConsumption
                        .Where(t => t.sector.SectorName == sector & t.energysource.SourceName == source & t.Year == year)
                        .Select(v => v.Value)
                        .First();
                updRecord.origValue = value;
                return View(updRecord);
            }
        }
    }
}