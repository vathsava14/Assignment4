using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public class UpdateRecord
    {
        public string County { get; set; }
        public string Population { get; set; }
        public int Value { get; set; }
        public int? origValue { get; set; }
    }
}