using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace basic_calculation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            NextButton.Clicked += NextButton_Clicked;
            NavigationPage.SetHasNavigationBar(this, false);

          //  Direction.Source = ImageSource.FromResource("basic_calculation.Image.DirectionImage.jpg"); 
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}