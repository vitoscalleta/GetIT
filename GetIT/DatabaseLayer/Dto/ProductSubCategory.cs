using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.DatabaseLayer.Dto
{
    public class ProductSubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Discount { get; set; }

        public int ProductCategory { get; set; }
    }
}
