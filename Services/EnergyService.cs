//using Assignment4.DataAccess;
//using Assignment4.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Assignment4.Services
//{
//    public class EnergyService : IAnnual
//    {
//        private Assignment4DbContext _context;
//        public EnergyService(Assignment4DbContext context)
//        {
//            _context = context;
//        }
//        public void Add(AnnualEnergyConsumption newconsump)
//        {
//            _context.Add(newconsump);
//            _context.SaveChanges();

//        }

//        public IEnumerable<AnnualEnergyConsumption> GetAll()
//        {
//            return _context.AnnualEnergyConsumption
//                .Include(energy => energy.sector)
//                .Include(energy => energy.energysource)
//                .Include(energy => energy.Year)
//                .Include(energy => energy.Value);
//        }

//        public AnnualEnergyConsumption GetbyId(int id)
//        {
//            return _context.AnnualEnergyConsumption
//                .Include(energy => energy.sector)
//                .Include(energy => energy.energysource)
//                .Include(energy => energy.Year)
//                .Include(energy => energy.Value)
//                .FirstOrDefault(energy => energy.ConsumptionId == id);
//        }

//        public string getenergysource(int id)
//        {
//            return _context.EnergySource.FirstOrDefault(energy => energy.EnergySourceId == id).SourceName;
                
//        }

//        public string getsector(int id)
//        {
//            return _context.Sector.FirstOrDefault(energy => energy.SectorId == id).SectorName;
//        }

//        public int getyear(int id)
//        {
//            return _context.AnnualEnergyConsumption.FirstOrDefault(energy => energy.ConsumptionId == id).Year;

//        }

//        public Decimal getvalue(int id)
//        {
//            return _context.AnnualEnergyConsumption.FirstOrDefault(energy => energy.ConsumptionId == id).Value;
//        }
//    }
//}