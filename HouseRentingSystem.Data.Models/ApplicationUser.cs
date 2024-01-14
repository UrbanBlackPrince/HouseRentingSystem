using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystem.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.RentedHouses = new HashSet<House>();  
        }

        public virtual ICollection<House> RentedHouses { get; set; }
    }
}
