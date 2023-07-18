using Newtonsoft.Json;

namespace Budget.BuisnessLogic.Models
{
    public class TokenResponse
    {
        [JsonProperty("id_token")]
        public string Token { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresToken { get; set; }

        public DateTime ExpiresIn { get; set; }
    }
}