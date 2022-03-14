using PhoneXamarin.Client.Services;
using PhoneXamarin.Client.Views;
using PhoneXamarin.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneXamarin.Client
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<AuthService>();
            if (Current.Properties.ContainsKey("token"))
            {
                AuthService auth = new AuthService();
                if ( auth.ValidateToken(Current.Properties["token"].ToString()).Result )
                {
                    MainPage = new AppShell();

                }
                else
                {
                    MainPage = new LoginPage();
                }
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
