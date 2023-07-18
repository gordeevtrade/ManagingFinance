using Budget.BuisnessLogic.Models;
using Budget.BuisnessLogic.Sevices.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Budget.BuisnessLogic.Sevices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private IConfigurationRoot _configuration;
        private string _clientId;
        private string _clientSecret;
        private string _redirectUri;
        private string _tokenUrl;
        private string _scope;

        private const string TokenEndpoint = "https://oauth2.googleapis.com/token";
        private const string ClientId = "683345051720-ocppanvknt5hr7giqjro50fh50ajlf6q.apps.googleusercontent.com";
        private const string ClientSecret = "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";
        private const string RedirectUri = "http://localhost:4200/callback";

        public GoogleAuthService()
        {
            //_configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json").Build();
            //_clientId = _configuration["GoogleAuth:ClientId"];
            //_clientSecret = _configuration["GoogleAuth:ClientSecretKey"];
            //_redirectUri = _configuration["GoogleAuth:RedirectUri"];
            //_tokenUrl = _configuration["GoogleAuth:TokenUrl"];
            //_scope = _configuration["GoogleAuth:Scope"];
        }

        public async Task<TokenResponse> ReturnGoogleToken(string code)
        {
            var responseContent = await RetrieveAccessTokenFromGoogle(code);

            return ExtractTokenResponseFromContent(responseContent);
        }

        private async Task<string> RetrieveAccessTokenFromGoogle(string code)
        {
            using (var httpClient = new HttpClient())
            {
                var tokenRequest = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", ClientId),
                new KeyValuePair<string, string>("client_secret", ClientSecret),
                new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

                var tokenResponse = await httpClient.PostAsync(TokenEndpoint, tokenRequest);

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Error while retrieving access token.");
                }

                return await tokenResponse.Content.ReadAsStringAsync();
            }
        }

        private TokenResponse ExtractTokenResponseFromContent(string responseContent)
        {
            var tokenContent = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            tokenContent.Email = ExtractEmailFromJwt(tokenContent.Token);
            tokenContent.ExpiresIn = ConvertExpireTimeToUtcDateTime(tokenContent.ExpiresToken);
            return tokenContent;
        }

        private DateTime ConvertExpireTimeToUtcDateTime(int expiresIn)
        {
            DateTime now = DateTime.UtcNow;
            return now.AddSeconds(expiresIn);
        }

        private string ExtractEmailFromJwt(string tokenId)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenId);
            return jwtToken.Claims.First(claim => claim.Type == "email").Value;
        }

        //public string GetAuthUrl()
        //{
        //    var responseType = "code";
        //    var state = Guid.NewGuid().ToString();
        //    var authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={_clientId}&redirect_uri={_redirectUri}&response_type={responseType}&scope={_scope}&state={state}";
        //    return authUrl;
        //}

        //public async Task<TokenResponse> GetToken(string code)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var tokenRequest = new FormUrlEncodedContent(new[]
        //        {
        //        new KeyValuePair<string, string>("code", code),
        //        new KeyValuePair<string, string>("client_id", ClientId),
        //        new KeyValuePair<string, string>("client_secret", ClientSecret),
        //        new KeyValuePair<string, string>("redirect_uri", RedirectUri),
        //        new KeyValuePair<string, string>("grant_type", "authorization_code")
        //    });

        //        var tokenResponse = await httpClient.PostAsync(TokenEndpoint, tokenRequest);

        //        if (!tokenResponse.IsSuccessStatusCode)
        //        {
        //            throw new Exception("Error while retrieving access token.");
        //        }
        //        var responseContent = await tokenResponse.Content.ReadAsStringAsync();

        //        var tokenContent = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        //        var handler = new JwtSecurityTokenHandler();
        //        var jwtToken = handler.ReadJwtToken(tokenContent.TokenId);
        //        string email = jwtToken.Claims.First(claim => claim.Type == "email").Value;

        //        tokenContent.Email = email;

        //        return tokenContent;
        //    }
        //}
    }
}