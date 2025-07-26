using ePizzaHub.Models.ApiModels.Response;

namespace ePizzaHub.Core.Contracts
{
    public interface IAuthService
    {
        Task<ValidateUserResponse> ValidateUserAsync(string username, string password);
    }
}

