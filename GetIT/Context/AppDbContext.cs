using GetIT.DatabaseLayer.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

        public DbSet<ProductCategory>   ProductCategories { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }
    }
}
