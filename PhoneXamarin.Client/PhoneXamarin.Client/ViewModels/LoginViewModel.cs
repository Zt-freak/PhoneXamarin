using PhoneXamarin.Client.Views;
using PhoneXamarin.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhoneXamarin.Client.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        private string _username;
        private string _password;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void OnLoginClicked(object obj)
        {
            TokenModel tokenInformation = await AuthService.Login(Username, Password);
            Application.Current.Properties["token"] = tokenInformation.Token;

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
