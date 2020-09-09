using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirMonitor.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            var loc =  GetLocalization();
            ApiConnectionTest(loc.Result.Latitude, loc.Result.Longitude);
        }


        async Task ApiConnectionTest(double latitude, double longitude)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://airapi.airly.eu/v2/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                client.DefaultRequestHeaders.Add("apikey", "RUbitPk3AFppjKNa2w5bMYC8wcloKGwy");

                var response = await client.GetAsync("https://airapi.airly.eu/v2/installations/nearest?lat="+latitude+"&lng="+longitude+"&maxDistanceKM=100&maxResults=3");
                var json_ = "[" + await response.Content.ReadAsStringAsync() + "]";
                Console.WriteLine("Zwracam z api "+ json_);
            }
        }
        async Task<Location> GetLocalization()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                location = await Geolocation.GetLocationAsync(request);
            }

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
            }

            return location;
        }

        private ICommand _NavPage;
        public ICommand NavPage => _NavPage ?? (_NavPage = new Command(NavPages));

        private void NavPages()
        {
            this.navigation.PushAsync(new DetailsPage());
        }
    }
}
