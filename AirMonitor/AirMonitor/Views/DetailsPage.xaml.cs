using AirMonitor.Models;
using AirMonitor.ViewModels;
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
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Measurement item)
        {
            InitializeComponent();

            var vm = BindingContext as DetailsViewModel;
            vm.Item = item;
        }

        private void HelpButton(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Wspólny roczny indeks jakości powietrza stanowi ogólną ocenę sytuacji w zakresie jakości powietrza w danym mieście przez cały rok w odnieseniu do norm europejskich.", "Zamknij");
        }
    }
}
