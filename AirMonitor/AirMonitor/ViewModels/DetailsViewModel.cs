using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AirMonitor.ViewModels
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int caqiValue = 50;
        public int CaqiValue
        {
            get => caqiValue;
            set => SetProperty(ref caqiValue, value);

        }

        private void SetProperty(ref int caqiValue, int value)
        {
            throw new NotImplementedException();
        }
    }



    

}
