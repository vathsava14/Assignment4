using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4.Models
{
    public class AnnualEnergyConsumption
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ConsumptionId { get; set; }
        public Sector sector { get; set; }
        public EnergySource energysource { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
