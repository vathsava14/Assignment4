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
    public class DeleteController : Controller
    {
        private readonly Assignment4DbContext dbContext;

        public DeleteController(Assignment4DbContext context)
        {
            dbContext = context;
        }

        IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public  IActionResult Index(string countyName, int totalPop, int bachelorPop)
        {
            Demographic DelRecord = dbContext.Demographics
                        .Where(d => d.County.CountyName == countyName & d.TotalPop == totalPop)
                        .First();
            
                DeleteRecord DelRecord1 = new DeleteRecord();
                DelRecord1.CountyName = countyName;
                DelRecord1.TotalPop = totalPop;
                DelRecord1.BachelorPop = bachelorPop;
                return View(DelRecord1); 
        }

        [HttpPost]
        public async Task<IActionResult> DelConfirm(string countyName, int totalPop, int bachelorPop, string confirmation)
        {
            Demographic DelRecord = dbContext.Demographics
                       .Where(d => d.County.CountyName == countyName & d.TotalPop == totalPop)
                       .First();
            if (confirmation == "Yes")
            {
                dbContext.Demographics.Remove(DelRecord);
                await dbContext.SaveChangesAsync();
                return View(DelRecord);
            }
            else
            {
                return View();
            }
            
        }
    }
}