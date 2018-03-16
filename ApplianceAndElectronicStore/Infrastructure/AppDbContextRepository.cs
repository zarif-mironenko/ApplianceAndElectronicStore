using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Infrastructure
{
    public class AppDbContextRepository
    {
        ApplicationDbContext context;

        public AppDbContextRepository(ApplicationDbContext contextService)
        {
            context = contextService;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await EF.CompileAsyncQuery((ApplicationDbContext db, int paramId)
                => context.Products.AsNoTracking()
                       .Include(p => p.Category)
                       .Include(p => p.Manufacturer)
                       .Include(p => p.ProductSpecificationValues)
                            .ThenInclude(psv => psv.ProductSpecification)
                                .ThenInclude(ps => ps.ProductSpecificationNumbersInOrder)
                       .FirstOrDefault(p => p.Id == paramId)
            ).Invoke(context, id);

            if (product != null) {
                product.ProductSpecificationValues = product.ProductSpecificationValues
                   .OrderBy(psv => psv.ProductSpecification.ProductSpecificationNumbersInOrder
                        .First(n => n.CategoryId == product.CategoryId).NumberInOrder)
                   .ToList();
            } // if

            return product;
        } // GetProductByIdAsync

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category category = await EF.CompileAsyncQuery((ApplicationDbContext db, int paramId)
                => context.Categories.AsNoTracking()
                        .Include(c => c.ProductSpecificationNumbersInOrder)
                            .ThenInclude(n => n.ProductSpecification)
                        .FirstOrDefault(c => c.Id == paramId)
            ).Invoke(context, id);

            if (category != null) {
                category.ProductSpecificationNumbersInOrder = category.ProductSpecificationNumbersInOrder
                                .OrderBy(n => n.NumberInOrder)
                                .ToList();
            } // if

            return category;
        } // GetCategoryByIdAsync
    }
}
