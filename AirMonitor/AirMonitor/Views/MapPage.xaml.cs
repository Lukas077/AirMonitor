using AirMonitor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views
{
    public partial class MapPage : ContentPage
    {
        private HomeViewModel _viewModel => BindingContext as HomeViewModel;

        public MapPage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);

        }
      
    }
}