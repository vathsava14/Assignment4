using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class CountyTotals
    {
        public List<CountyPopulationTotal> data { get; set; }
    }
    public class CountyPopulationTotal
    {
        public string County { get; set; }
        public string Population { get; set; }
        public int Value { get; set; }
    }
}
