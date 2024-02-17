using HouseRentingSystem.Web.ViewModels.CategoryViewModel.Interfaces;

namespace HouseRentingSystem.Web.ViewModels.CategoryViewModel
{
    public class CategoryDetailsViewModel : ICategoryDetailsModel
    {
        public string Name { get; set; } = null!;
        public int Id { get; set; } 
    }
}
