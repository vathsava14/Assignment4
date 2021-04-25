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
        private readonly Assignment4DbContext dbContext;

        public CreateController(Assignment4DbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string countyName, string totalPop, string bachelorPop)
        {
            CreationConfirmation confirmation = new CreationConfirmation();
            confirmation.Heading = "New Record Successfully Created";
            confirmation.WasSuccessful = true;

            try
            {
                County county = dbContext.Counties.Where(c => c.CountyName == countyName).First();
                Demographic newRecord = new Demographic();
                newRecord.County = county;
                newRecord.TotalPop = Convert.ToInt32(totalPop);
                newRecord.BachelorPop = Convert.ToInt32(bachelorPop);
                dbContext.Demographics.Add(newRecord);
                await dbContext.SaveChangesAsync();
                Demographic confirmRecord = dbContext.Demographics.Where(c => c.County.CountyName == countyName & c.TotalPop == Convert.ToInt32(totalPop) & c.BachelorPop == Convert.ToInt32(bachelorPop)).First();
                confirmation.DemographicData = confirmRecord;
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
