using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Web.ViewModels.Agent;
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

        public async Task<HouseDetailsViewModel?> GetDetailsByIdAsync(string houseId)
        {
            House house = await dbContext
                .Houses
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == houseId);


            return new HouseDetailsViewModel
            {
                Id = house.Id,
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                PricePerMounth = house.PricePerMounth,
                IsRented = house.RenterId.HasValue,
                Description = house.Description,
                Category = house.Category.Name,
                Agent = new AgentInfoOnHouseViewModel()
                {
                    Email = house.Agent.User.Email,
                    PhoneNumber = house.Agent.PhoneNumber
                }
            };
        }

        public async Task<bool> ExistByIdAsync(string houseId)
        {
            bool result = await this.dbContext
                .Houses
                .Where(h => h.IsActive)
                .AnyAsync(h => h.Id.ToString() == houseId);

            return result;
        }

        public async Task<HouseViewModel> GetHouseForEditByIdAsync(string houseId)
        {
            House house = await dbContext
                .Houses
                .Include(h => h.Category)
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == houseId);

            return new HouseViewModel
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMounth = house.PricePerMounth,
                CategoryId = house.CategoryId,
            };

        }

        public async Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId)
        {
            House house = await this.dbContext
                .Houses
                .Where(h => h.IsActive)
               .FirstAsync(h => h.Id == Guid.Parse(houseId));

            return house.AgentId.ToString() == agentId;
        }

        public async Task EditHouseByIdAndFormModel(string houseId, HouseViewModel viewModel)
        {
            House house = await this.dbContext
                .Houses
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == houseId);

            house.Title = viewModel.Title;
            house.Address = viewModel.Address;
            house.Description = viewModel.Description;  
            house.ImageUrl = viewModel.ImageUrl;
            house.PricePerMounth = viewModel.PricePerMounth;
            house.CategoryId = viewModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }
    }
}

