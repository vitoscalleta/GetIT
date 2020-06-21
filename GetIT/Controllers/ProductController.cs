using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetIT.DatabaseLayer.Dto;
using GetIT.DatabaseLayer.Repository.Interface;
using GetIT.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GetIT.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _IProductRepository;

        public ProductController(IProductRepository iProductRepository)
        {
            _IProductRepository = iProductRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddProduct()
        {
            AddProductVM addProductVM = new AddProductVM();
            loadDropDowns();
            return View();
        }

        [Authorize]
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
                    Description = addProductVM.Description,
                    Price = addProductVM.Price
                };
                _IProductRepository.AddProduct(product);
                ModelState.Clear();
                addProductVM = new AddProductVM();
                ModelState.AddModelError("Info","Product Added");
            }
            loadDropDowns();
            return View(addProductVM);
        }

        private void loadDropDowns()
        {
            ViewBag.ProductCategories = _IProductRepository.GetAllProductCategories();
            ViewBag.ProductSubCategories = _IProductRepository.GetAllProductSubCategories();
        }
    }
}