using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneXamarin.Service
{
    public class PhoneService : BaseService, IPhoneService
    {
        public async Task<IEnumerable<Phone>> GetAll()
        {
            HttpResponseMessage response =  await GetAsync("phone/all");
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Phone>>(responseString);
        }
    }
}
