using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class HousePreDeleteViewModel
    {
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;
    }
}
