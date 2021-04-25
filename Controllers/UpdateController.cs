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
    public class UpdateController : Controller
    {
        private readonly Assignment4DbContext dbContext;

        public UpdateController(Assignment4DbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string CountyName, int TotalPop, int BachelorPop, int? value, string? valueStr)
        {
            Demographic modRecord = dbContext.Demographics
                        .Where(d => d.County.CountyName == CountyName & d.TotalPop == TotalPop)
                        .First();
            if (modRecord.BachelorPop == value)
            {
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.CountyName = CountyName;
                updRecord.TotalPop = TotalPop;
                updRecord.NewBachelorPop = (int)value;
                updRecord.BachelorPop = (int)value;
                return View(updRecord);
            }
            else
            {
                modRecord.BachelorPop = Convert.ToInt32(valueStr);
                dbContext.Demographics.Update(modRecord);
                await dbContext.SaveChangesAsync();
                UpdateRecord updRecord = new UpdateRecord();
                updRecord.CountyName = CountyName;
                updRecord.TotalPop = TotalPop;
                updRecord.NewBachelorPop = dbContext.Demographics
                        .Where(d => d.County.CountyName == CountyName & d.TotalPop == TotalPop & d.BachelorPop == BachelorPop)
                        .Select(t => t.BachelorPop)
                        .First();
                updRecord.BachelorPop = (int)value;
                return View(updRecord);
            }
        }
    }
}