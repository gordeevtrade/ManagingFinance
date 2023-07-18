using Domain.DAL.Entity;

namespace Domain.ServicesInterface
{
    public interface IUserService
    {
        Task<bool> IsUserRegistered(string email);

        Task<User> RegisterUser(string email, string password);

        Task<User> GetUserByEmail(string email);

        bool VerifyPassword(string password, string hashedPassword);
    }
}