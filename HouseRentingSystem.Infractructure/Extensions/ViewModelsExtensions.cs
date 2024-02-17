using HouseRentingSystem.Web.ViewModels.CategoryViewModel.Interfaces;

namespace HouseRentingSystem.Infractructure.Extensions
{
    public static class ViewModelsExtensions
    {
        public static string GetUrlInformation(this ICategoryDetailsModel model)
        {
            return model.Name.Replace(" ", "-");
        }
    }
}
