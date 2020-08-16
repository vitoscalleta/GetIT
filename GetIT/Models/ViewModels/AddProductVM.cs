using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GetIT.Models.ViewModels
{
    public class AddProductVM
    {
        [Required]
        [Display(Name="Product Name")]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name = "Product Category")]
        public int ProductCategory { get; set; }
        
        public List<string> ProductCategoryList { get; set; }
        [Display(Name = "Product Sub-Category")]
        public int ProductSubCategory { get; set; }
        
        public List<string> ProductSubCategoryList { get; set; }

        public IFormFile Images { get; set; }

    }

    
}
