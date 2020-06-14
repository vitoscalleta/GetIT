using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetIT.DatabaseLayer.Dto;
using GetIT.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace GetIT.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductVM addProductVM)
        {
            if(ModelState.IsValid)
            {
                Product product = new Product
                {
                    ProductName = addProductVM.ProductName,
                    Category = addProductVM.ProductCategory,
                    SubCategory = addProductVM.ProductSubCategory,
                };

            }
            return View(addProductVM);
        }
    }
}