using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public class SectorSourceDetails
    {
        public string Sector { get; set; }
        public string Source { get; set; }
        public List<SectorSourceAnnual> data { get; set; }
    }

    public class SectorSourceAnnual
    {
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}