using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneXamarin.Service
{
    public class AuthService : BaseService, IAuthService
    {
        public string Jwt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<TokenModel> Login(string username, string password)
        {
            return await PostAsync<TokenModel>("login", new
            {
                Username = username,
                Password = password,
            });
        }
        public async Task<bool> ValidateToken(string token)
        {
            var result = await GetAsync<ValidationModel>("validate");

            if (result.Valid)
            {
                return true;
            }
            return false;
        }
    }
}
