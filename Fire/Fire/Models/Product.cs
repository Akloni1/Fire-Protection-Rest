using System;
using System.Collections.Generic;

#nullable disable

namespace Fire.Models
{
    public partial class Product
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
    }
}
