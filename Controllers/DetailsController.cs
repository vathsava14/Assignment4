using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Assignment4.DataAccess;
using Assignment4.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assignment4.Controllers
{
    public class DetailsController : Controller
    {
        private readonly Assignment4DbContext _context;

        public DetailsController(Assignment4DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string sector, string source)
        {
            SectorSourceDetails details = new SectorSourceDetails();
            details.Sector = sector;
            details.Source = source;
            List<SectorSourceAnnual> list = new List<SectorSourceAnnual>(); 
            var sectorSourceData = _context.AnnualEnergyConsumption.Where(t => t.sector.SectorName == sector & t.energysource.SourceName == source).OrderByDescending(y => y.Year);
            foreach (AnnualEnergyConsumption dbRow in sectorSourceData)
            {
                SectorSourceAnnual listRow = new SectorSourceAnnual();
                listRow.Year = dbRow.Year;
                listRow.Value = dbRow.Value;
                list.Add(listRow);
            }
            details.data = list;
            return View(details);
        }
    }
}