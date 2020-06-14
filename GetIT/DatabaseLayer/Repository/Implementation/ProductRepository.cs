﻿using GetIT.Context;
using GetIT.DatabaseLayer.Dto;
using GetIT.DatabaseLayer.Repository.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        public Product GetProductById(int id)
        {
            return _DbContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public List<Product> GetProductsByName(string name)
        {
            return _DbContext.Products.Where(product => product.ProductName == name).ToList();
        }

        public List<Product> GetProductsByCatergory(string category)
        {
            return _DbContext.Products.Where(product => product.Category == category).ToList();
        }

        public List<Product> GetProducsBySubCategory(string subCategory)
        {
            return _DbContext.Products.Where(product => product.SubCategory == subCategory).ToList();
        }
    }
}
