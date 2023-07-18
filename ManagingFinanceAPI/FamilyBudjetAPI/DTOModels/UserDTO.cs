using System.ComponentModel.DataAnnotations;

namespace ManagingFinanceAPI.DTOModels
{
    public class UserDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}