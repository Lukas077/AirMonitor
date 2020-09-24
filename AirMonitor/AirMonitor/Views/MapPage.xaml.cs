using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using AirMonitor.ViewModels;

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

         void InfoWindow_ItemTapped(object sender, Xamarin.Forms.Maps.PinClickedEventArgs e)
        {
               _viewModel.InfoWindowClickedCommand.Execute((sender as Xamarin.Forms.Maps.Pin).Address);
        }


    }
}