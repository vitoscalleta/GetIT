using GetIT.DatabaseLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.DatabaseLayer.Repository.Interface
{
  public  interface IProductRepository
    {
        void AddProduct(Product product);

        Product GetProductById(int id);

        List<Product> GetProductsByName(string name);

        List<Product> GetProductsByCatergory(int category);

        List<Product> GetProductsBySubCategories(int subCategory);

        List<ProductCategory> GetAllProductCategories();

        List<ProductSubCategory> GetAllProductSubCategories();

        List<ProductSubCategory> GetProductSubCategoriesByCategory(int categoryID);

        int GetNextId();
    }
}
