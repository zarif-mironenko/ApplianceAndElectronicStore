using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Infrastructure;
using ApplianceAndElectronicStore.Models.Entities;
using ApplianceAndElectronicStore.Models.ProductsViewModels;
using ApplianceAndElectronicStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplianceAndElectronicStore.Controllers
{
    public class ProductsController : Controller
    {
        readonly ApplicationDbContext context;
        readonly AppDbContextRepository repository;
        int pageSize;

        public ProductsController(ApplicationDbContext contextService,
                                  AppDbContextRepository repositoryService)
        {
            context = contextService;
            repository = repositoryService;
            pageSize = 9;
        }

        public async Task<IActionResult> Index(string category, ProductSearchFiltersViewModel filtersViewModel, int? page)
        {
            ViewBag.FiltersViewModel = filtersViewModel;

            var products = context.Products
                    .Include(p => p.Manufacturer)
                    .Where(p => category == null ||
                           p.Category.NameForUrl == category);

            if (filtersViewModel.MaxProdPrice > 0) {
                products = products
                    .Where(p => p.Price >= filtersViewModel.MinProdPrice &&
                           p.Price <= filtersViewModel.MaxProdPrice);
            } // if

            if (filtersViewModel.Manufacturers != null &&
                filtersViewModel.Manufacturers.Values.Any(m => m.IsSelected)) {
                products = products
                    .Where(p => filtersViewModel.Manufacturers
                        .Any(m => m.Key == p.ManufacturerId &&
                             m.Value.IsSelected)
                );
            } // if

            if (filtersViewModel.ProdSpecsWithValues != null &&
                filtersViewModel.ProdSpecsWithValues.Values
                    .Any(pswv => pswv.Values.Any(psv => psv.IsSelected))) {
                foreach (var item in filtersViewModel.ProdSpecsWithValues) {
                    products = products
                        .Where(p => p.ProductSpecificationValues
                            .Any(pPsv => pPsv.ProductSpecificationId == item.Key &&
                                 item.Value.Any(psv => psv.Key == pPsv.Id &&
                                                psv.Value.IsSelected)
                            )
                    );
                } // foreach
            } // if

            var viewModel = new ProductsListViewModel {
                Products = await products.OrderBy(p => p.Id)
                              .Skip(((page ?? 1) - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync(),
                PagingInfo = new PagingInfo {
                    CurrentPage = page ?? 1,
                    ItemsPerPage = pageSize,
                    TotalItems = await products.CountAsync(),
                    ElementSizeClass = "pagination-lg"
                },
                CurrentCategory = context.Categories
                    .FirstOrDefault(c => c.NameForUrl == category)
            };

            ViewBag.SelectedCategory = viewModel.CurrentCategory;

            viewModel.PagingInfo.PageUrlValues["category"] = category;

            return View(viewModel);
        }

        public async Task<IActionResult> ProductDetails(int id, string category, int? page)
        {
            ViewBag.SelectedCategory = context.Categories
                .FirstOrDefault(c => c.NameForUrl == category);
            ViewBag.CurrentPageNumber = page;

            Product product = await repository.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Search(string searchTerm, int? page)
        {
            ViewBag.SearchTerm = "Поиск" + (searchTerm != null ? $" - {searchTerm}" : null);

            var products = context.Products
                              .Include(p => p.Manufacturer)
                              .Where(p => p.Name.Contains(searchTerm) ||
                                     p.Description.Contains(searchTerm) ||
                                     p.ProductSpecificationValues
                                      .Any(psv => psv.Value.Contains(searchTerm)));

            return View(new ProductsListViewModel {
                Products = await products.OrderBy(p => p.Id)
                              .Skip(((page ?? 1) - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync(),
                PagingInfo = new PagingInfo {
                    CurrentPage = page ?? 1,
                    ItemsPerPage = pageSize,
                    TotalItems = await products.CountAsync(),
                    ElementSizeClass = "pagination-lg",
                    PageUrlValues = new Dictionary<string, object> {
                        ["searchTerm"] = searchTerm
                    }
                }
            });
        }
    }
}