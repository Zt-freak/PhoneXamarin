using PhoneXamarin.Client.Models;
using PhoneXamarin.Service;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneXamarin.Client.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command OrderCommand { get; }

        private Phone item;
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel()
        {
            OrderCommand = new Command(OnItemSelected);

        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                item = await PhoneService.GetById(int.Parse(itemId));
                Id = item.Id.ToString();
                Text = item.Type;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnItemSelected(object obj)
        {
            Order newOrder = new Order()
            {
                CustomerId = int.Parse(Application.Current.Properties["id"].ToString())
            };
            PhoneService.OrderPhone(newOrder, item);
            await Application.Current.MainPage.DisplayAlert("Alert", "Your order has been placed", "OK");
        }
    }
}
