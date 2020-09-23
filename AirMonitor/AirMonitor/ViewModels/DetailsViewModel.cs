using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using AirMonitor.Models;
using System.Linq;

namespace AirMonitor.ViewModels
{
    /* public class DetailsViewModel : BaseViewModel
     {

         public DetailsViewModel()
         {

         }


         int caqiValue = 50;
         public int CaqiValue
         {
             get => caqiValue;
             set => SetProperty(ref caqiValue, value);

         }

         string caqiText = "Świetna jakość!";
         public string CaqiText
         {
             get => caqiText;
             set => SetProperty(ref caqiText, value);

         }

         string caqiDesc = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
         public string CaqiDesc
         {
             get => caqiDesc;
             set => SetProperty(ref caqiDesc, value);

         }

         int pmValue25 = 34;
         public int PmValue25
         {
             get => pmValue25;
             set => SetProperty(ref pmValue25, value);

         }

         int pmPercent25 = 137;
         public int PmPercent25
         {
             get => pmPercent25;
             set => SetProperty(ref pmPercent25, value);

         }

         int pmValue10 = 67;
         public int PmValue10
         {
             get => pmValue10;
             set => SetProperty(ref pmValue10, value);

         }

         int pmPercent10 = 135;
         public int PmPercent10
         {
             get => pmPercent10;
             set => SetProperty(ref pmPercent10, value);

         }

         double humidity = 0.65;
         public double Humidity
         {
             get => humidity;
             set => SetProperty(ref humidity, value);

         }



         int pressure = 1000;
         public int Pressure
         {
             get => pressure;
             set => SetProperty(ref pressure, value);

         }
     }
     */

    public class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel()
        {
        }

        private Measurement _item;
        public Measurement Item
        {
            get => _item;
            set
            {
                SetProperty(ref _item, value);

                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            if (Item?.Current == null) return;
            var current = Item?.Current;
            var index = current.Indexes?.FirstOrDefault(c => c.Name == "AIRLY_CAQI");
            var values = current.Values;
            var standards = current.Standards;

            CaqiValue = (int)Math.Round(index?.Value ?? 0);
            CaqiText = index.Description;
            CaqiDesc = index.Advice;
            PmValue25 = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM25")?.Value ?? 0);
            PmValue10 = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM10")?.Value ?? 0);
            Humidity = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "HUMIDITY")?.Value ?? 0);
            Pressure = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PRESSURE")?.Value ?? 0);
            PmPercent25 = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM25")?.Percent ?? 0);
            PmPercent10 = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM10")?.Percent ?? 0);
        }

        int caqiValue = 50;
        public int CaqiValue
        {
            get => caqiValue;
            set => SetProperty(ref caqiValue, value);

        }

        string caqiText = "Świetna jakość!";
        public string CaqiText
        {
            get => caqiText;
            set => SetProperty(ref caqiText, value);

        }

        string caqiDesc = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
        public string CaqiDesc
        {
            get => caqiDesc;
            set => SetProperty(ref caqiDesc, value);

        }

        int pmValue25 = 34;
        public int PmValue25
        {
            get => pmValue25;
            set => SetProperty(ref pmValue25, value);

        }

        int pmPercent25 = 137;
        public int PmPercent25
        {
            get => pmPercent25;
            set => SetProperty(ref pmPercent25, value);

        }

        int pmValue10 = 67;
        public int PmValue10
        {
            get => pmValue10;
            set => SetProperty(ref pmValue10, value);

        }

        int pmPercent10 = 135;
        public int PmPercent10
        {
            get => pmPercent10;
            set => SetProperty(ref pmPercent10, value);

        }

        double humidity = 0.65;
        public double Humidity
        {
            get => humidity;
            set => SetProperty(ref humidity, value);

        }



        int pressure = 1000;
        public int Pressure
        {
            get => pressure;
            set => SetProperty(ref pressure, value);

        }
    }






}

