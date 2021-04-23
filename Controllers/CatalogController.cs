//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Assignment4.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using Assignment4.DataModels.Catalog;

//namespace Assignment4.Controllers
//{
//    public class CatalogController : Controller
//    {
//        private IAnnual _energy;
//        public CatalogController(IAnnual energy)
//        {
//            _energy = energy;
//        }

//        public IActionResult Index()
//        {
//            var energyModels = _energy.GetAll();
//            var listingResult = energyModels
//                .Select(result => new EnergyIndexListingModel
//                {
//                    Id = result.ConsumptionId,
//                    Sector = _energy.getsector(result.ConsumptionId),
//                    EnergySource = _energy.getenergysource(result.ConsumptionId),
//                    Year = _energy.getyear(result.ConsumptionId),
//                    Value = _energy.getvalue(result.ConsumptionId)

//                }) ;
//            var model = new EnergyIndexModel()
//            {
//                Energies = listingResult
//            };
//            return View(model);
//        }
//    }
//}
