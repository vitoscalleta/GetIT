using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetIT.Models.ViewModels
{
    public class AddProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public int ProductCategory { get; set; }

        public List<string> ProductCategoryList { get; set; }

        public int ProductSubCategory { get; set; }

        public List<string> ProductSubCategoryList { get; set; }

        public IFormFile Images { get; set; }

    }

    
}
