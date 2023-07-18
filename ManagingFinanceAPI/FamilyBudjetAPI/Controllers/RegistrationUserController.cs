using AutoMapper;
using Domain.ServicesInterface;
using ManagingFinanceAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace ManagingFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public RegistrationUserController(IUserService userService, IMapper mapper, IAuthService auth)
        {
            _userService = userService;

            _authService = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            try
            {
                await _userService.RegisterUser(userDto.Email, userDto.Password);

                return Ok(new { Message = "Пользователь успешно зарегистрирован." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userLoginDto)
        {
            try
            {
                var token = await _authService.LoginUser(userLoginDto.Email, userLoginDto.Password);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Неправильная почта или пароль.");
            }
        }
    }
}