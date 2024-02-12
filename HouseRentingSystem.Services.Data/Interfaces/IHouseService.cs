using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Services.Data.Models.Statistics;
using HouseRentingSystem.Web.ViewModels.Home;
using HouseRentingSystem.Web.ViewModels.House;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

        Task<string> CreateAndReturnIdAsync(HouseViewModel viewModel, string agentId);

        Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryViewModel queryModel);

        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);

        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);

        Task<bool> ExistByIdAsync(string houseId);

        Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

        Task<HouseViewModel> GetHouseForEditByIdAsync(string houseId);

        Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId);

        Task EditHouseByIdAndFormModelAsync(string houseId, HouseViewModel viewModel);

        Task<HousePreDeleteViewModel> GetHouseForDeleteByIdAsync(string houseId);
        Task DeleteHouseByIdAsync(string houseId);

        Task<bool> IsRentedByIdAsync(string houseId);

        Task RentHouseAsync (string houseId, string userId);

        Task<bool> IsRentedByUserWithIdAsync(string houseId, string userId);

        Task LeaveHouseAsync(string houseId);

        Task<StatisticsServiceModel> GetStatisticsAsyc();

    }
}
