using HouseRentingSystem.Web.ViewModels.CategoryViewModel;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<HouseSelectCategoryViewModel>> AllCategoriesAsync();
        Task<IEnumerable<AllCategoriesViewModel>> AllCategoriesForListAsync();

        Task<bool> ExistsByIdAsync(int Id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

        Task<CategoryDetailsViewModel> GetDetailsByIdAsync(int Id);
    }
}
