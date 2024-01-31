using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;
using HouseRentingSystem.Web.ViewModels.House.Enums;
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
                 .Where(h => h.IsActive)
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

        public async Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryViewModel queryModel)
        {
            IQueryable<House> housesQuery = this.dbContext.Houses.AsQueryable();

            // Filter by category
            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                housesQuery = housesQuery.Where(h => h.Category.Name == queryModel.Category);
            }

            // Filter by search string
            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                housesQuery = housesQuery.Where(h =>
                    EF.Functions.Like(h.Title, wildCard) ||
                    EF.Functions.Like(h.Address, wildCard) ||
                    EF.Functions.Like(h.Description, wildCard));
            }

            // Apply sorting
            housesQuery = queryModel.HouseSorting switch
            {
                HouseSorting.Newest => housesQuery.OrderBy(h => h.CreatedOn),
                HouseSorting.Oldest => housesQuery.OrderByDescending(h => h.CreatedOn),
                HouseSorting.PriceAscending => housesQuery.OrderBy(h => h.PricePerMounth),
                HouseSorting.PriceDescending => housesQuery.OrderByDescending(h => h.PricePerMounth),
                _ => housesQuery.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.CreatedOn)
            };

            // Get the paginated houses
            IEnumerable<HouseAllViewModel> allHouses = await housesQuery
                .Where(h => h.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.HousesPerPage)
                .Take(queryModel.HousesPerPage)
                .Select(h => new HouseAllViewModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMounth = h.PricePerMounth,
                    IsRented = h.RenterId.HasValue
                })
                .ToArrayAsync();

            // Count the total houses
            int totalHouses = housesQuery.Count();

            return new AllHousesFilteredAndPagedServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = allHouses
            };
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<HouseAllViewModel> allAgentHouses = await this.dbContext
                .Houses
                .Where(h => h.IsActive)
                .Where(h => h.Agent.Id.ToString() == agentId)
                .Select(h => new HouseAllViewModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMounth = h.PricePerMounth,
                    IsRented = h.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentHouses;
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<HouseAllViewModel> allUserHouses = await this.dbContext
               .Houses
               .Where(h => h.IsActive)
               .Where(h => h.RenterId.HasValue &&
               h.RenterId.ToString() == userId)
               .Select(h => new HouseAllViewModel()
               {
                   Id = h.Id,
                   Title = h.Title,
                   Address = h.Address,
                   ImageUrl = h.ImageUrl,
                   PricePerMounth = h.PricePerMounth,
                   IsRented = h.RenterId.HasValue
               })
               .ToArrayAsync();

            return allUserHouses;
        }
    }
}

