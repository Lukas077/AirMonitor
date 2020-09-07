using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AirMonitor.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void HelpButton(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Wspólny roczny indeks jakości powietrza stanowi ogólną ocenę sytuacji w zakresie jakości powietrza w danym mieście przez cały rok w odnieseniu do norm europejskich.", "Zamknij");
        }
    }
}
