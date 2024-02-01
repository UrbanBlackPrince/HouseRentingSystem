using HouseRentingSystem.Infractructure.Extensions;
using HouseRentingSystem.Services.Data.Interfaces;
using HouseRentingSystem.Services.Data.Models.House;
using HouseRentingSystem.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystem.Common.NotificationMessagesConstants;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;
        public HouseController(ICategoryService categoryService, IAgentService agentService, IHouseService houseService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.houseService = houseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryViewModel queryModel)
        {
            AllHousesFilteredAndPagedServiceModel serviceModel =
                await this.houseService.AllAsync(queryModel);

            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";
                return this.RedirectToAction("Become", "Agent");
            }

            HouseViewModel viewModel = new HouseViewModel()
            {
                Categories = await this.categoryService.AllCategoriesAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseViewModel viewModel)
        {
            bool isAgent =
              await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new houses!";
                return this.RedirectToAction("Become", "Agent");
            }

            bool categoryExists = await this.categoryService.ExistsByIdAsync(viewModel.CategoryId);

            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(viewModel.CategoryId), "Selected category doest not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                viewModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(viewModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
                await this.houseService.CreateAsync(viewModel, agentId!);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured whle trying to add your new house.");
                viewModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(viewModel);
            }

            return this.RedirectToAction("All", "House");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool houseExist = await this.houseService.ExistByIdAsync(id);
            if (!houseExist)
            {
                this.TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            }

           HouseDetailsViewModel viewModel = await this.houseService
                .GetDetailsByIdAsync(id);

           return View(viewModel);  
        }


        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses =
                new List<HouseAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(userId);

            if(isUserAgent)
            {
                string? agentId =
                    await this.agentService.GetAgentIdByUserIdAsync(userId);

                myHouses.AddRange(await this.houseService.AllByAgentIdAsync(agentId!));
            }
            else
            {
                myHouses.AddRange(await this.houseService.AllByAgentIdAsync(userId));
            }
            return this.View(myHouses);  
        }

    }
}
