using Budget.DAL.Repositories.Interfaces;
using Domain.DAL.Entity;
using Domain.ServicesInterface;

namespace ManagingFinance.BuisnessLogic.Sevices
{
    public class UserService : IUserService
    {
        private IUnitOfWOrk _unitOfWork;

        public UserService(IUnitOfWOrk unitOfWOrk)
        {
            _unitOfWork = unitOfWOrk;
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            return await _unitOfWork.UserRepository.AnyAsync(u => u.Email == email);
        }

        public async Task<User> RegisterUser(string email, string password)
        {
            bool isUserRegistered = await IsUserRegistered(email);
            if (isUserRegistered)
            {
                throw new InvalidOperationException("Пользователь с такой почтой уже зарегистрирован.");
            }

            string passwordHash = HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash
            };
            _unitOfWork.UserRepository.Add(user);

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Email == email);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
    }
}