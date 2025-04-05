using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessTracker.Models;
using FitnessTracker.Services;

namespace FitnessTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutsPlans : ContentPage
    {
        private readonly WorkoutService _workoutService = new WorkoutService();

        public WorkoutsPlans()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                int loggedInUserId = SessionManager.LoggedInUser.Id;

                var allWorkouts = await _workoutService.GetAllWorkoutsAsync();
                var userWorkouts = allWorkouts.Where(w => w.UserId == loggedInUserId).ToList();

                workoutsListView.ItemsSource = userWorkouts;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to load workouts: {ex.Message}", "OK");
            }
        }

        private async void OnClickedCreateWorkout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateWorkout());
        }
    }
}
