using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.ViewModels.CategoryViewModel;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HouseRentingDbContext dbContext;
        public CategoryService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<HouseSelectCategoryViewModel>> AllCategoriesAsync()
        {
            IEnumerable<HouseSelectCategoryViewModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new HouseSelectCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync()
        {
            IEnumerable<AllCategoriesViewModel> allCategories = await this.dbContext
                 .Categories
                 .AsNoTracking()
                 .Select(c => new AllCategoriesViewModel
                 {
                     Id = c.Id,
                     Name = c.Name,
                 })
                 .ToArrayAsync();

            return allCategories;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .Categories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }

        public async Task<bool> ExistsByIdAsync(int Id)
        {
           bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == Id);

            return result;
        }

        public async Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int Id)
        {
            Category category = await this.dbContext
                .Categories
                .FirstAsync(x => x.Id == Id);

            CategoryDetailsViewModel viewModel = new CategoryDetailsViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };

            return viewModel;
        }
    }
}
