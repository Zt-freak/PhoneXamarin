using PhoneXamarin.Client.Models;
using PhoneXamarin.Client.Views;
using PhoneXamarin.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneXamarin.Client.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Phone _selectedItem;

        public ObservableCollection<Phone> Phones { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Phone> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Phones = new ObservableCollection<Phone>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Phone>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Phones.Clear();
                IEnumerable<Phone> phones = await PhoneService.GetAll();
                foreach (var phone in phones)
                {
                    Phones.Add(phone);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Phone SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Phone phone)
        {
            if (phone == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={phone.Id}");
        }
    }
}