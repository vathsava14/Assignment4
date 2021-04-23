using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Assignment4.DataAccess;
using Assignment4.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assignment4.Controllers
{
    public class ExploreController : Controller
    {
        private readonly Assignment4DbContext _context;

        public ExploreController(Assignment4DbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SectorSourceTotal> list = new List<SectorSourceTotal>();
            foreach (var sector in _context.Sector.OrderBy(s => s.SectorName))
            {
                foreach (var source in _context.EnergySource.OrderBy(s => s.SourceName))
                {
                    List<Decimal> sectorSourceData = _context.AnnualEnergyConsumption
                                                    .Where(t => t.sector.SectorName == sector.SectorName & t.energysource.SourceName == source.SourceName)
                                                    .Select(v => v.Value)
                                                    .ToList();
                    Decimal total = sectorSourceData.Sum();
                    SectorSourceTotal listRow = new SectorSourceTotal();
                    listRow.Sector = sector.SectorName;
                    listRow.Source = source.SourceName;
                    listRow.Value = total;
                    list.Add(listRow);
                }
            }
            SectorTotals aggregates = new SectorTotals();
            aggregates.data = list; 
            return View(aggregates);
        }
    }
}