using HouseRentingSystem.Infractructure.Extensions;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AllCategoriesViewModel> viewModel = await this.categoryService.AllCategoriesForListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id, string information)
        {
            bool categoryExists = await this.categoryService.ExistsByIdAsync(Id);
            if(!categoryExists)
            {
                return NotFound();  
            }

            CategoryDetailsViewModel viewModel = await this.categoryService.GetDetailsByIdAsync(Id);

            if (!categoryExists || viewModel.GetUrlInformation() != information)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
