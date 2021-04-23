﻿using System;
using System.Collections.Generic;

namespace Assignment4.Models
{
    public class UpdateRecord
    {
        public string Sector { get; set; }
        public string Source { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
        public Decimal? origValue { get; set; }
    }
}