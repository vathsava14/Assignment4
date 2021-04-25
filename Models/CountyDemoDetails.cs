using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class CountyDemoDetails
    {
        public string CountyName { get; set; }
        public int TotalPop { get; set; }
        public List<DemographicPopData> data { get; set; }
    }
    
    public class DemographicPopData
    {
        public int BachelorPop { get; set; }
    }
}
