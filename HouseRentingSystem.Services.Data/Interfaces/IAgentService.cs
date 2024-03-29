﻿using HouseRentingSystem.Web.ViewModels.Agent;

namespace HouseRentingSystem.Services.Data.Interfaces
{
    public interface IAgentService
    {
        Task<bool> AgentExistsByUserIdAsync(string userId);
        Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> HasRentsByUserIdAsync(string userId);
        Task Create(string userId, BecomeAgentViewModel model);
        Task<string?> GetAgentIdByUserIdAsync(string userId);
        Task<bool> HasHouseWithIdAsync(string? agentId, string houseId);
    }
}
