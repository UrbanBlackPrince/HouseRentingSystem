using HouseRentingSystem.Web.ViewModels.CategoryViewModel;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<HouseSelectCategoryViewModel>> AllCategoriesAsync();
    }
}
