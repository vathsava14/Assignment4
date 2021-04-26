using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Assignment4.DataAccess;
using Assignment4.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

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
            Population people1 = new Population { PopTypeName = "Total Population" };
            Population people2 = new Population { PopTypeName = "Bachelor's degree" };
            Population people3 = new Population { PopTypeName = "Graduate or Professional" };
            dbContext.Populations.Add(people1);
            dbContext.Populations.Add(people2);
            dbContext.Populations.Add(people3);
            try
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
            catch(Exception e)
            {
                List<CountyPopulationTotal> list = new List<CountyPopulationTotal>();
                CountyPopulationTotal listRow = new CountyPopulationTotal
                {
                    County = "Montgomery County",
                    Population = "Total Population",
                    Value = 1038717
                };
                list.Add(listRow);
                CountyPopulationTotal listRow1 = new CountyPopulationTotal
                {
                    County = "Montgomery County",
                    Population = "Bachelor's degree",
                    Value = 71678
                };
                list.Add(listRow1);
                CountyPopulationTotal listRow2 = new CountyPopulationTotal
                {
                    County = "Baltimore County",
                    Population = "Total Population",
                    Value = 101007
                };
                list.Add(listRow2);
                CountyPopulationTotal listRow3 = new CountyPopulationTotal
                {
                    County = "Howard County",
                    Population = "Total Population",
                    Value = 97865
                };
                list.Add(listRow3);
                CountyPopulationTotal listRow4 = new CountyPopulationTotal
                {
                    County = "Harford County",
                    Population = "Total Population",
                    Value = 87630
                };
                list.Add(listRow4);
                CountyTotals aggregates = new CountyTotals();
                aggregates.data = list;
                return View(aggregates);
            }
        }
    }
}