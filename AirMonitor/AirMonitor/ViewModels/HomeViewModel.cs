using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using AirMonitor.Models;
using AirMonitor.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace AirMonitor.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            //var loc =  GetLocalization();
            // ApiConnectionTest(loc.Result.Latitude, loc.Result.Longitude);
            Initialize(false);

        }


        private async Task Initialize(bool forceRefresh)
        {
            IsBusy = true;

            await LoadData(forceRefresh);

            IsBusy = false;
        }

        private async Task LoadData(bool forceRefresh)
        {
            var location = await GetLocation();
            var data = await Task.Run(async () =>
            {
                var installations = await GetInstallations(location, forceRefresh, maxResults: 3);
                return await GetMeasurementsForInstallations(installations, forceRefresh);
            });

            Items = new List<Measurement>(data);
        }
        /*
        private async Task Initialize(bool forceRefresh)
        {
            IsBusy = true;
            var location = await GetLocation();
            var installations = await GetInstallations(location, maxResults: 3);
            var data = await GetMeasurementsForInstallations(installations);
            Items = new List<Measurement>(data);

            await LoadData(forceRefresh);

            IsBusy = false;
        }

       */
        private ICommand _goToDetailsCommand;
        public ICommand GoToDetailsCommand => _goToDetailsCommand ?? (_goToDetailsCommand = new Command<Measurement>(OnGoToDetails));

        private void OnGoToDetails(Measurement item)
        {
            this.navigation.PushAsync(new DetailsPage(item));
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(async () => await OnRefreshCommand()));

        private async Task OnRefreshCommand()
        {
            IsRefreshing = true;

            await LoadData(true);

            IsRefreshing = false;
        }


        private List<Measurement> _items;
        public List<Measurement> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }


        private async Task<IEnumerable<Installation>> GetInstallations(Location location, bool forceRefresh, double maxDistanceInKm = 50, int maxResults = -1)
        {
            if (location == null)
            {
                System.Diagnostics.Debug.WriteLine("No location data.");
                return null;
            }

            IEnumerable<Installation> result;

            var savedMeasurements = App.DbHelper.GetMeasurements();

                if (forceRefresh || ShouldUpdateData(savedMeasurements))
                {

                    var query = GetQuery(new Dictionary<string, object>
                {
                    { "lat", 52.173421 }, // na sztywno location.Latitude 52.173421
                    { "lng", 20.997043 }, // na sztywno location.Longitude 20.997043
                    { "maxDistanceKM", maxDistanceInKm },
                    { "maxResults", maxResults }
                });
                    var url = GetAirlyApiUrl(App.AirlyApiInstallationUrl, query);

                    // var response = await GetHttpResponseAsync<IEnumerable<Installation>>(url);
                    result = await GetHttpResponseAsync<IEnumerable<Installation>>(url);


                    System.Diagnostics.Debug.WriteLine($"WWW: {url}");

                    // return response;

                    App.DbHelper.SaveInstallations(result);

                }

                else
                {
                    result = App.DbHelper.GetInstallations();
                }

               return result;
        }

            private async Task<IEnumerable<Measurement>> GetMeasurementsForInstallations(IEnumerable<Installation> installations, bool forceRefresh)
            {
            if (installations == null)
            {
                System.Diagnostics.Debug.WriteLine("No installations data.");
                return null;
            }

            var measurements = new List<Measurement>();
            var savedMeasurements = App.DbHelper.GetMeasurements();

            if (forceRefresh || ShouldUpdateData(savedMeasurements))
            {
                foreach (var installation in installations)
            {
                var query = GetQuery(new Dictionary<string, object>
                {
                    { "installationId", installation.Id }
                });
                var url = GetAirlyApiUrl(App.AirlyApiMeasurementUrl, query);

                var response = await GetHttpResponseAsync<Measurement>(url);

                    System.Diagnostics.Debug.WriteLine($"WWWw: {url}");


                    if (response != null)
                {
                    response.Installation = installation;
                    response.CurrentDisplayValue = (int)Math.Round(response.Current?.Indexes?.FirstOrDefault()?.Value ?? 0);
                    measurements.Add(response);
                }
            }

                App.DbHelper.SaveMeasurements(measurements);
            }
            else
            {
                measurements = savedMeasurements.ToList();
            }

            foreach (var measurement in measurements)
            {
                measurement.CurrentDisplayValue = (int)Math.Round(measurement.Current?.Indexes?.FirstOrDefault()?.Value ?? 0);
            }

            return measurements;
        }

        /* ------------------Moje--------------------
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
        */



        private bool ShouldUpdateData(IEnumerable<Measurement> savedMeasurements)
        {
            var isAnyMeasurementOld = savedMeasurements.Any(s => s.Current.TillDateTime.AddMinutes(60) < DateTime.UtcNow);

            return savedMeasurements.Count() == 0 || isAnyMeasurementOld;
        }


        private string GetQuery(IDictionary<string, object> args)
        {
            if (args == null) return null;

            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var arg in args)
            {
                if (arg.Value is double number)
                {
                    query[arg.Key] = number.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    query[arg.Key] = arg.Value?.ToString();
                }
            }

            return query.ToString();
        }

        private string GetAirlyApiUrl(string path, string query)
        {
            var builder = new UriBuilder(App.AirlyApiUrl);
            builder.Port = -1;
            builder.Path += path;
            builder.Query = query;
            string url = builder.ToString();

            return url;
        }

        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(App.AirlyApiUrl);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            client.DefaultRequestHeaders.Add("apikey", App.AirlyApiKey);
            return client;
        }

        private async Task<T> GetHttpResponseAsync<T>(string url)
        {
            try
            {
                var client = GetHttpClient();
                var response = await client.GetAsync(url);

                if (response.Headers.TryGetValues("X-RateLimit-Limit-day", out var dayLimit) &&
                    response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var dayLimitRemaining))
                {
                    System.Diagnostics.Debug.WriteLine($"Day limit: {dayLimit?.FirstOrDefault()}, remaining: {dayLimitRemaining?.FirstOrDefault()}");
                }

                switch ((int)response.StatusCode)
                {
                    case 200:
                        var content = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Content: {content}");

                        var result = JsonConvert.DeserializeObject<T>(content);
                        System.Diagnostics.Debug.WriteLine($"Result: {result}");
                        return result;
                    case 429: // too many requests
                        System.Diagnostics.Debug.WriteLine("Too many requests");
                        break;
                    default:
                        var errorContent = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine($"Response error: {errorContent}");
                        return default;
                }
            }
            catch (JsonReaderException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return default;
        }

        private async Task<Location> GetLocation()
        {
            try
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
            // Handle different exceptions separately, for example to display different messages to the user
            catch (FeatureNotSupportedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (FeatureNotEnabledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (PermissionException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }

        
    }
}
