using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public class SectorTotals
    {
        public List<SectorSourceTotal> data { get; set; }
    }

    public class SectorSourceTotal
    {
        public string Sector { get; set; }
        public string Source { get; set; }
        public Decimal Value { get; set; }
    }
}