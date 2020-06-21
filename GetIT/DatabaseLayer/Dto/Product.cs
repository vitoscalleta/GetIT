using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GetIT.DatabaseLayer.Dto
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
       
        public string Description { get; set; }

        public double Price { get; set; }
        
        public int Category { get; set; }

        public int SubCategory { get; set; }
    }
}
