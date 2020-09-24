using AirMonitor.Models;
using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace AirMonitor.Views
{
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel => BindingContext as HomeViewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            _viewModel.GoToDetailsCommand.Execute(e.Item as Measurement);
        }
    }
}