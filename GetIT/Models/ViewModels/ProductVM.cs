using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Models.ViewModels
{
    public class ProductVM
    {
        public string ProductName { get; set; }
      
        public string Description { get; set; }

        public double Price { get; set; }

        public string ProductCategory { get; set; }

        public string ProductSubCategory { get; set; }

    }
}
