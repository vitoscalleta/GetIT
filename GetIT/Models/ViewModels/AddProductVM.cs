using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static GetIT.Utility.Classes.Enumerators;

namespace GetIT.ViewModels.Models
{
    public class AddProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public string ProductCategory { get; set; }

        public List<string> ProductCategoryList { get; set; }

        public string ProductSubCategory { get; set; }

        public List<string> ProductSubCategoryList { get; set; }

    }

    
}
