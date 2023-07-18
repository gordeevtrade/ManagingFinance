using Budget.BuisnessLogic.Models;

namespace Budget.BuisnessLogic.Sevices.Interface
{
    public interface IGoogleAuthService
    {
        Task<TokenResponse> ReturnGoogleToken(string code);
    }
}