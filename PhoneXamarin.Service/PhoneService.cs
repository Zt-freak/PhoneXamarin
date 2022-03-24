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
            return JsonConvert.DeserializeObject<PhoneListModel>(responseString).Results;
        }

        public async Task<Phone> GetById(int id)
        {
            HttpResponseMessage response = await GetAsync($"phone/{id}");
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Phone>(responseString);
        }

        public async void OrderPhone(Order order, Phone phone)
        {
            HttpResponseMessage response = await PostAsync($"order", order);
            string responseString = await response.Content.ReadAsStringAsync();
            Order submittedOrder = JsonConvert.DeserializeObject<Order>(responseString);
            response = await PostAsync($"order/add", new
            {
                OrderId = submittedOrder.Id,
                ProductId = phone.Id
            });
        }
    }

    public class PhoneListModel
    {
        public List<Phone> Results { get; set; }
    }
}
