using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneXamarin.Service
{
    public class AuthService : BaseService, IAuthService
    {
        public string Jwt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<TokenModel> Login(string username, string password)
        {
            HttpResponseMessage response = await PostAsync("login", new
            {
                Username = username,
                Password = password
            });

            string responseString = await response.Content.ReadAsStringAsync();
            TokenModel result = JsonConvert.DeserializeObject<TokenModel>(responseString);

            return result;
        }

        public async Task<TokenModel> Register(string username, string password, string email)
        {
            HttpResponseMessage response = await PostAsync("register", new
            {
                Username = username,
                Password = password,
                Email = email
            });

            string responseString = await response.Content.ReadAsStringAsync();
            TokenModel result = JsonConvert.DeserializeObject<TokenModel>(responseString);

            return result;
        }

        public async Task<bool> ValidateToken(string token)
        {
            HttpResponseMessage response = await GetAsync("validate");

            string responseString = await response.Content.ReadAsStringAsync();
            ValidationModel result = JsonConvert.DeserializeObject<ValidationModel>(responseString);

            if (result.Valid)
            {
                return true;
            }
            return false;
        }
    }
}
