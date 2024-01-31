using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Web.ViewModels.House
{
    public class HouseAllViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Mounthly Price")]
        public decimal PricePerMounth { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}

