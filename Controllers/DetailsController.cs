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
        private readonly Assignment4DbContext dbContext;

        public DetailsController(Assignment4DbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string county, string population)
        {
            CountyPopulationDetails details = new CountyPopulationDetails();
            details.County = county;
            details.Population = population;
            List<CountyPopulationValue> list = new List<CountyPopulationValue>();
            var data = dbContext.Demographics.Where(d => d.county.CountyName == county & d.population.PopTypeName == population).OrderByDescending(c => c.county.CountyName);
            foreach (Demographic dbRow in data)
            {
                CountyPopulationValue listRow = new CountyPopulationValue();
                listRow.Value = dbRow.Value;
                list.Add(listRow);
            }
            details.data = list;
            return View(details);
        }
    }
}