using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class Demographic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DemographicId { get; set; }
        public County County { get; set; }
        public int TotalPop { get; set; }
        public int BachelorPop { get; set; }
    }
}
