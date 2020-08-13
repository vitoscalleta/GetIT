using GetIT.Context;
using GetIT.DatabaseLayer.Dto;
using GetIT.DatabaseLayer.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GetIT.DatabaseLayer.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _DbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public int GetNextId()
        {
            return _DbContext.Products.Max(a => a.Id) + 1;
        }

        public void AddProduct(Product product)
        {
            _DbContext.Products.Add(product);
            _DbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _DbContext.Products.Update(product);
            _DbContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _DbContext.Products.FirstOrDefault(product => product.Id == id);
        }


        public List<Product> GetProductsByName(string name)
        {
            return _DbContext.Products.Where(product => product.ProductName == name).ToList();
        }

        public List<Product> GetProductsByCatergory(int category)
        {
            return _DbContext.Products.Where(product => product.Category == category).ToList();
        }

        public List<Product> GetProductsBySubCategories(int subCategory)
        {
            return _DbContext.Products.Where(product => product.SubCategory == subCategory).ToList();
        }

        public List<ProductCategory> GetAllProductCategories()
        {
            return _DbContext.ProductCategories.ToList();
        }

        public List<ProductSubCategory> GetAllProductSubCategories()
        {
            return _DbContext.ProductSubCategories.ToList();
        }

        public List<ProductSubCategory> GetProductSubCategoriesByCategory(int categoryID)
        {
            return _DbContext.ProductSubCategories.Where(p => p.ProductCategory == categoryID).ToList();
        }
        

        public void AddProductImage(ProductImages productImages)
        {
            _DbContext.ProductImages.Add(productImages);
            _DbContext.SaveChanges();
        }

    }
}
