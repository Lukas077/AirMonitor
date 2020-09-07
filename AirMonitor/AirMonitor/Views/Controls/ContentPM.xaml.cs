using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentPM : StackLayout
    {
        public ContentPM()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(ContentPM),
            defaultValue: null);   

        public string Title
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        public static readonly BindableProperty ControlContentProperty = BindableProperty.Create(
            propertyName: nameof(ControlContent),
            returnType: typeof(View),
            declaringType: typeof(ContentPM),
            defaultValue: null);

        public View ControlContent
        {
            get => (View)GetValue(ControlContentProperty);
            set => SetValue(ControlContentProperty, value);
        }
    }

   
}