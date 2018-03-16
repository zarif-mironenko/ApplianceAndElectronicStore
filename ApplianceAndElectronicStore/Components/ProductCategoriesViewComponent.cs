using ApplianceAndElectronicStore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApplianceAndElectronicStore.Components {

    public class ProductCategoriesViewComponent : ViewComponent
    {
        ApplicationDbContext context;

        public ProductCategoriesViewComponent(ApplicationDbContext contextService)
        {
            context = contextService;
        }

        public IViewComponentResult Invoke()
        {
            //if (int.TryParse($"{RouteData.Values["categoryId"]}", out int catId))
            ViewBag.SelectedCategory = $"{RouteData.Values["category"]}";

            return View(context.Categories.ToList());
        }
    }
}
