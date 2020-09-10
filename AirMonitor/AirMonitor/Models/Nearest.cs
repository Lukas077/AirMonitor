using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    class Nearest
    {
        public int Id { get; set; }

        public Location Location { get; set; }

        public string address { get; set; }

        public double elevation { get; set; }

        [JsonProperty(PropertyName = "airly")]

        public bool IsAirly { get; set;  }

    }
}
