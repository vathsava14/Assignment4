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
        public async Task<IActionResult> Index(string countyName, string popTypeName, int value)
        {
            CreationConfirmation confirmation = new CreationConfirmation();
            confirmation.Heading = "New Record Successfully Created";
            confirmation.WasSuccessful = true;

            try
            {
                //County county = dbContext.Counties.Where(c => c.CountyName == countyName).First();
                //Population population = dbContext.Populations.Where(p => p.PopTypeName == popTypeName).First();
                County places = new County { CountyName = countyName };
                Population people = new Population { PopTypeName = popTypeName };
                Demographic newRecord = new Demographic
                {
                    Value = value,
                    population = people,
                    county = places
                };
                //newRecord.county = county;
                //newRecord.population = population;
                //newRecord.Value = Convert.ToInt32(Value);
                //dbContext.Demographics.Add(newRecord);
                dbContext.Counties.Add(places);
                dbContext.Populations.Add(people);
                dbContext.Demographics.Add(newRecord);
                dbContext.SaveChanges();
                Demographic confirmRecord = dbContext.Demographics.Where(d => d.county.CountyName == countyName & d.population.PopTypeName == popTypeName & d.Value == value).First();
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
