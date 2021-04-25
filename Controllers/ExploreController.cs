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
        private readonly Assignment4DbContext dbContext;

        public ExploreController(Assignment4DbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            List<CountyPopulationTotal> list = new List<CountyPopulationTotal>();
            foreach (var county in dbContext.Counties.OrderBy(c => c.CountyName))
            {
                foreach (var population in dbContext.Populations.OrderBy(p => p.PopTypeName))
                {
                    List<int> countyPopulationData = dbContext.Demographics
                                                    .Where(d => d.county.CountyName == county.CountyName & d.population.PopTypeName == population.PopTypeName)
                                                    .Select(v => v.Value)
                                                    .ToList();
                    int total = countyPopulationData.Sum();
                    CountyPopulationTotal listRow = new CountyPopulationTotal();
                    listRow.County = county.CountyName;
                    listRow.Population = population.PopTypeName;
                    listRow.Value = total;
                    list.Add(listRow);
                }
            }
            CountyTotals aggregates = new CountyTotals();
            aggregates.data = list; 
            return View(aggregates);
        }
    }
}