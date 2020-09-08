using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using AirMonitor.Views;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }


        private ICommand _NavPage;
        public ICommand NavPage => _NavPage ?? (_NavPage = new Command(NavPages));

        private void NavPages()
        {
            this.navigation.PushAsync(new DetailsPage());
        }
    }
}
