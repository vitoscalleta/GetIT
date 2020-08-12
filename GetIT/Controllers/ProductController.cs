using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GetIT.DatabaseLayer.Dto;
using GetIT.DatabaseLayer.Repository.Interface;
using GetIT.Helpers.Interface;
using GetIT.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GetIT.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _IProductRepository;
        IImageStorageHelper _ImageStorageHelper;

        public ProductController(IProductRepository iProductRepository, IImageStorageHelper imageStorageHelper)
        {
            _IProductRepository = iProductRepository;
            _ImageStorageHelper = imageStorageHelper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddProduct()
        {            
            AddProductVM addProductVM = new AddProductVM();
            loadDropDowns();
            return View(addProductVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductVM addProductVM)
        {
            if(ModelState.IsValid)
            {
                //string ImageName = UploadImage(addProductVM.Images);

                Product product = new Product
                {
                    ProductName = addProductVM.ProductName,
                    Category = addProductVM.ProductCategory,
                    SubCategory = addProductVM.ProductSubCategory,
                    Description = addProductVM.Description,
                    Price = addProductVM.Price
                };
                _IProductRepository.AddProduct(product);

                //Upload image
                await UploadImages(addProductVM.Images, product.Id);


                ModelState.Clear();
                addProductVM = new AddProductVM();
                ModelState.AddModelError(,"Product Added");
            }
            loadDropDowns();
            return View(addProductVM);
        }

        private void loadDropDowns()
        {
            ViewBag.ProductCategories = _IProductRepository.GetAllProductCategories();
            ViewBag.ProductSubCategories = _IProductRepository.GetAllProductSubCategories();
        }

        public ActionResult GetSubCategories(int Id)
        {
            var productSubCategories = _IProductRepository.GetProductSubCategoriesByCategory(Id);
            return Json(productSubCategories);
        }

        [HttpGet]
        public ActionResult ProductList(int subCategoryId)
        {

            List<Product> productList = _IProductRepository.GetProductsBySubCategories(subCategoryId);
            List<ProductSubCategory> productCategories = _IProductRepository.GetAllProductSubCategories();

            List<ProductVM> productListVM = productList.Join(productCategories,
                                                            prod => prod.SubCategory,
                                                            subCat => subCat.Id,
                                                            (prod, subCat) => new ProductVM()                                           { ProductName = prod.ProductName,
                                                              ProductSubCategory = subCat.Name,
                                                              Description = prod.Description,
                                                              Price = prod.Price
                                                            }).ToList();                         
            return View(productListVM);
        }


        private async Task<bool> UploadImages(IFormFile image, int productId)
        {
            bool isUploaded = false;

            if (image == null)
                return false;           
   
            if(_ImageStorageHelper.IsImage(image))
            {
                using (Stream stream = image.OpenReadStream())
                {
                    string imageName = $"{Guid.NewGuid()}_{image.FileName}"; 
                    isUploaded = await _ImageStorageHelper.UploadImagesToStorage(stream, imageName);
                    if(isUploaded)
                    {
                        ProductImages productImage = new ProductImages
                        {
                            ProductId = productId,
                            ImageFileName = imageName,
                            UploadTimeStamp = DateTime.Now
                        };
                        _IProductRepository.AddProductImage(productImage);
                    }
                }
            }
            
            return isUploaded;
        }
    }
}