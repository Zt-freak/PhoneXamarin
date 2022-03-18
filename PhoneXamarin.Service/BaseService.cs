using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhoneXamarin.Service
{
    public class BaseService
    {
        private string _baseAddress = DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:5001/api/"
                    : "https://localhost:44396/api/";
        private string _token = string.Empty;

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await ExecuteCall(path);
        }

        public async Task<HttpResponseMessage> PostAsync(string apiPath, object content)
        {
            return await ExecuteCall(apiPath, content);
        }

        private async Task<HttpResponseMessage> ExecuteCall(string apiPath, object content = null)
        {
            try
            {
                using (var client = new HttpClient(GetInsecureHandler()))
                {
                    if (Application.Current.Properties.ContainsKey("token"))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["token"].ToString());
                    }
                    HttpResponseMessage response;

                    if (content == null)
                    {
                        response = await client.GetAsync($"{_baseAddress}{apiPath}");
                    }
                    else
                    {
                        string serialized = JsonConvert.SerializeObject(content);
                        StringContent stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

                        response = await client.PostAsync($"{_baseAddress}{apiPath}", stringContent);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return response;
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