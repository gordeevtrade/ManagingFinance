using Budget.BuisnessLogic.Models;

namespace Domain.ServicesInterface
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginUser(string email, string password);
    }
}