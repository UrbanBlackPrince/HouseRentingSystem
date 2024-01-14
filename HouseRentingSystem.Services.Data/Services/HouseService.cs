using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.ViewModels.Home;

namespace HouseRentingSystem.Services.Data.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
