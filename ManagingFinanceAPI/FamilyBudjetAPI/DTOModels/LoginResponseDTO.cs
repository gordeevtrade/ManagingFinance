namespace ManagingFinanceAPI.DTOModels
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string Email { get; set; }
    }
}