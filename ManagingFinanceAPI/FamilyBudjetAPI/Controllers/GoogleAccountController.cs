using AutoMapper;
using Budget.BuisnessLogic.Sevices.Interface;
using ManagingFinanceAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAccountController : ControllerBase
    {
        private IGoogleAuthService _googleAuthService;
        private readonly IMapper _mapper;

        public GoogleAccountController(IGoogleAuthService authService, IMapper mapper)
        {
            _googleAuthService = authService;
            _mapper = mapper;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> ReturnGoogleToken(string code)
        {
            try
            {
                var tokenResponse = await _googleAuthService.ReturnGoogleToken(code);
                var loginResponse = _mapper.Map<LoginResponseDTO>(tokenResponse);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}