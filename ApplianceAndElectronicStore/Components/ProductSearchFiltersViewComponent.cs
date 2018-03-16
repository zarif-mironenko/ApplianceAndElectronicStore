using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Models.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Components
{
    public class ProductSearchFiltersViewComponent : ViewComponent
    {
        ApplicationDbContext context;

        public ProductSearchFiltersViewComponent(ApplicationDbContext contextService)
        {
            context = contextService;
        }

        public IViewComponentResult Invoke(int categoryId, ProductSearchFiltersViewModel viewModel)
        {
            var catProducts = context.Products
                .AsNoTracking()
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductSpecificationValues)
                    .ThenInclude(psv => psv.ProductSpecification)
                        .ThenInclude(ps => ps.ProductSpecificationNumbersInOrder)
                .Where(p => p.CategoryId == categoryId);

            var prodSpecs = catProducts
                    .SelectMany(p => p.ProductSpecificationValues
                        .Where(psv => psv.ProductSpecification.Name != "Дополнительно" &&
                               psv.Value != "-")
                        .Select(psv => psv.ProductSpecification))
                    .Distinct()
                    .OrderBy(ps => ps.ProductSpecificationNumbersInOrder
                            .First(psn => psn.ProductSpecificationId == ps.Id).NumberInOrder
                    ).ToList();

            foreach (var item in prodSpecs) {
                var prodSVs = context.ProductSpecificationsValues
                    .AsNoTracking()
                    .Include(psv => psv.Product)
                    .GroupBy(psv => new { psv.Product.CategoryId, psv.ProductSpecificationId, psv.Value })
                    .Where(g => g.Key.ProductSpecificationId == item.Id &&
                                    g.Key.CategoryId == categoryId &&
                                    g.Key.Value != "-")
                    .Select(g => g.First())
                    .ToList();

                item.ProductSpecificationValues.AddRange(prodSVs);
            }

            if (viewModel.ProdSpecsWithValues == null) {
                viewModel = new ProductSearchFiltersViewModel {
                    ProductSpecifications = prodSpecs.ToDictionary(ps => ps.Id),
                    ProdSpecsWithValues = prodSpecs
                        .ToDictionary(ps => ps.Id, ps => ps.ProductSpecificationValues
                            .ToDictionary(psv => psv.Id, 
                                psv => new ProductSpecificationValueViewModel {
                                    ProductSpecificationValue = psv
                                })
                    ),
                    Manufacturers = catProducts
                        .Select(p => p.Manufacturer)
                        .Distinct()
                        .ToDictionary(m => m.Id, m => new ManufacturerViewModel {
                            Manufacturer = m
                        }),
                    MinProdPrice = catProducts.Min(p => p.Price),
                    MaxProdPrice = catProducts.Max(p => p.Price)
                };
            } else {
                viewModel.ProdSpecsWithValues = prodSpecs
                        .ToDictionary(ps => ps.Id, ps => ps.ProductSpecificationValues
                            .ToDictionary(psv => psv.Id,
                                psv => new ProductSpecificationValueViewModel {
                                    ProductSpecificationValue = psv,
                                    IsSelected = viewModel.ProdSpecsWithValues[ps.Id][psv.Id].IsSelected
                                })
                );

                viewModel.Manufacturers = catProducts
                        .Select(p => p.Manufacturer)
                        .Distinct()
                        .ToDictionary(m => m.Id, m => new ManufacturerViewModel {
                            Manufacturer = m,
                            IsSelected = viewModel.Manufacturers[m.Id].IsSelected
                });
            } // if

            return View(viewModel);
        }
    }
}
