using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Common.EntityValidationConstants.User;

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
