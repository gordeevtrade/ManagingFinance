using Budget.BuisnessLogic.Models;
using Domain.DAL.Entity;
using Domain.ServicesInterface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagingFinance.BuisnessLogic.Sevices
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private IConfiguration _configuration;
        private string secretKey;
        private int expirationMinutes;

        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            secretKey = _configuration["JwtSettings:SecretKey"];
            expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"]);
        }

        public async Task<TokenResponse> LoginUser(string email, string password)
        {
            var user = await _userService.GetUserByEmail(email);

            if (user == null || !_userService.VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Неправильная почта или пароль.");
            }

            return GenerateJwtToken(user);
        }

        private TokenResponse GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = GetSecurityTokenDescriptor(user, key);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var loginResponse = new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Email = user.Email
            };

            return loginResponse;
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(User user, byte[] key)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}