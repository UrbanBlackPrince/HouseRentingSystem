using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Data.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext dbContext;

        public HouseService(HouseRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync()
        {
            IEnumerable<IndexViewModel> lastThreeHouses = await this.dbContext
                 .Houses
                 .OrderByDescending(x => x.CreatedOn)
                 .Take(3)
                 .Select(h => new IndexViewModel()
                 {
                     Id = h.Id.ToString(),
                     Title = h.Title,
                     ImageUrl = h.ImageUrl,
                 })
                 .ToArrayAsync();

            return lastThreeHouses;
        }

        public async Task CreateAsync(HouseViewModel viewModel, string agentId)
        {
            House newHouse = new House()
            {
                Title = viewModel.Title,
                Address = viewModel.Address,
                Description = viewModel.Description,
                ImageUrl = viewModel.ImageUrl,
                PricePerMounth = viewModel.PricePerMounth,
                CategoryId = viewModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            await this.dbContext.Houses.AddAsync(newHouse);
            await this.dbContext.SaveChangesAsync();

        }
    }
}
