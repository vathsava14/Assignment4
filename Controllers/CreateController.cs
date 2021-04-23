using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Assignment4.DataAccess;
using Assignment4.Models;
using System.Threading.Tasks;

namespace Assignment4.Controllers
{
    public class CreateController : Controller
    {
        private readonly Assignment4DbContext _context;

        public CreateController(Assignment4DbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SectorName, string SourceName, string Year, string Value)
        {
            CreationConfirmation confirmation = new CreationConfirmation();
            confirmation.Heading = "New Record Successfully Created";
            confirmation.WasSuccessful = true;

            try
            {
                Sector sector = _context.Sector.Where(s => s.SectorName == SectorName).First();
                EnergySource energySource = _context.EnergySource.Where(e => e.SourceName == SourceName).First();
                AnnualEnergyConsumption newRecord = new AnnualEnergyConsumption();
                newRecord.sector = sector;
                newRecord.energysource = energySource;
                newRecord.Year = Convert.ToInt32(Year);
                newRecord.Value = Convert.ToDecimal(Value);
                _context.AnnualEnergyConsumption.Add(newRecord);
                await _context.SaveChangesAsync();
                AnnualEnergyConsumption confirmRecord = _context.AnnualEnergyConsumption.Where(c => c.sector.SectorName == SectorName & c.energysource.SourceName == SourceName & c.Year == Convert.ToInt32(Year) & c.Value == Convert.ToDecimal(Value)).First();
                confirmation.ConsumptionData = confirmRecord;
            }
            catch (Exception e)
            {
                confirmation.Heading = "Record Creation Failed";
                confirmation.WasSuccessful = false;
            }
            
            return View(confirmation);
        }
    }
}
