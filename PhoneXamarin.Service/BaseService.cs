using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PhoneXamarin.Service
{
    public class BaseService
    {
        private string _baseAddress = DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:5001/api/"
                    : "https://localhost:44396/api/";
        //private string _baseAddress = "https://localhost:44396";
        public async Task<T> GetAsync<T>(string path)
        {
            return await ExecuteCall<T>(path);
        }

        public async Task<T> PostAsync<T>(string apiPath, object content)
        {
            return await ExecuteCall<T>(apiPath, content);
        }

        private async Task<T> ExecuteCall<T>(string apiPath, object content = null)
        {
            try
            {
                using (var client = new HttpClient(GetInsecureHandler()))
                {
                    HttpResponseMessage response;

                    if (content == null)
                    {
                        response = await client.GetAsync($"{_baseAddress}{apiPath}");
                    }
                    else
                    {
                        string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                        StringContent stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

                        response = await client.PostAsync($"{_baseAddress}{apiPath}", stringContent);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);

                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            throw new Exception("Something went wrong");
        }


        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;

                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };

            return handler;
        }
    }
}