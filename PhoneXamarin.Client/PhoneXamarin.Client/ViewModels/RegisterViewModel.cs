using PhoneXamarin.Client.Views;
using PhoneXamarin.Service;
using Xamarin.Forms;

namespace PhoneXamarin.Client.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; }
        private string _username;
        private string _password;
        private string _email;

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
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
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private async void OnRegisterClicked(object obj)
        {
            TokenModel tokenInformation = await AuthService.Register(Username, Password, Email);
            Application.Current.Properties["token"] = tokenInformation.Token;

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
