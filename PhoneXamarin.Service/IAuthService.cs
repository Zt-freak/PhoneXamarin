using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneXamarin.Service
{
    public interface IAuthService
    {
        string Jwt { get; set; }
        Task<TokenModel> Login(string username, string password);
        Task<TokenModel> Register(string username, string password, string email);
    }
}
