using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class CountyPopulationDetails
    {
        public string County { get; set; }
        public string Population { get; set; }
        public List<CountyPopulationValue> data { get; set; }
    }
    
    public class CountyPopulationValue
    {
        public int Value { get; set; }
    }
}
