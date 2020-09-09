using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace AirMonitor.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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





        private void SetProperty(ref string caqiText, string value)
        {
        }

        private void SetProperty(ref int caqiValue, int value)
        {
        }

        private void SetProperty(ref double humidity, double value)
        {
        }

        


    }







}

