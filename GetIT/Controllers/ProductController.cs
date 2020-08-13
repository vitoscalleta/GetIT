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
using Microsoft.AspNetCore.Mvc;

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
                string imagePath = await UploadImages(addProductVM.Images, product.Id);
                product.ImagePath = imagePath;
                _IProductRepository.UpdateProduct(product);

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

        public ActionResult GetSubCategories(int Id)
        {
            var productSubCategories = _IProductRepository.GetProductSubCategoriesByCategory(Id);
            return Json(productSubCategories);
        }

        [HttpGet]
        public async Task<ActionResult> ProductList(int subCategoryId)
        {

            List<Product> productList = _IProductRepository.GetProductsBySubCategories(subCategoryId);
            List<ProductSubCategory> productCategories = _IProductRepository.GetAllProductSubCategories();

            List<ProductVM> productListVM =  productList.Join(productCategories,
                                                            prod => prod.SubCategory,
                                                            subCat => subCat.Id,
                                                             (prod, subCat) => new ProductVM() { ProductName = prod.ProductName,
                                                              ProductSubCategory = subCat.Name,
                                                              Description = prod.Description,
                                                              Price = prod.Price,
                                                              ProductID = prod.Id,
                                                              ImageURL = prod.ImagePath
                                                            }).ToList();      
            
            foreach(var productVM in productListVM)
            {
                if(productVM.ImageURL != null && productVM.ImageURL != string.Empty)
                        productVM.ImageDataURL = @"data:image/png;base64," + await _ImageStorageHelper.GetImageData(productVM.ImageURL);
            }             
            return View(productListVM);
        }


        private async Task<string> UploadImages(IFormFile image, int productId)
        {
            bool isUploaded = false;
            string imageName = string.Empty;
            if (image == null)
                return string.Empty;           
   
            if(_ImageStorageHelper.IsImage(image))
            {
                using (Stream stream = image.OpenReadStream())
                {
                    imageName = $"{Guid.NewGuid()}_{image.FileName}"; 
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
            
            return imageName;
        }


       
    }
}