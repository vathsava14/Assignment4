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
        public IActionResult Index(string countyName, int totalPop)
        {
            CountyDemoDetails details = new CountyDemoDetails();
            details.CountyName = countyName;
            details.TotalPop = totalPop;
            List<DemographicPopData> list = new List<DemographicPopData>();
            var data = dbContext.Demographics.Where(d => d.County.CountyName == countyName & d.TotalPop == totalPop).OrderBy(c => c.County.CountyName);
            foreach (Demographic dbRow in data)
            {
                DemographicPopData listRow = new DemographicPopData();
                listRow.BachelorPop = dbRow.BachelorPop;
                list.Add(listRow);
            }
            details.data = list;
            return View(details);
        }
    }
}