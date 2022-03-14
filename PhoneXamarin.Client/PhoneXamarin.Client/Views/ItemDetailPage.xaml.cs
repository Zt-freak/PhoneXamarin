using PhoneXamarin.Client.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhoneXamarin.Client.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}