using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Xamarin.Forms;
using FitnessTracker.Views;


namespace FitnessTracker.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            AnimateElements();
        }

        private async void AnimateElements()
        {
            await Task.Delay(500);
            await TaglineLabel.FadeTo(1, 1000);
            await FeatureCard1.FadeTo(1, 1000);
            await FeatureCard2.FadeTo(1, 1000);
            await GetStartedButton.FadeTo(1, 1000);
            await SignInButton.FadeTo(1, 1000);
        }

        private async void OnGetStartedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }

    }
}