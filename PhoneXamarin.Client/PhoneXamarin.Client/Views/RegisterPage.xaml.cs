using PhoneXamarin.Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneXamarin.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }
    }
}