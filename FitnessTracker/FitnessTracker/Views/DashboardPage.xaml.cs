using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessTracker.Services;
using FitnessTracker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FitnessTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        private readonly WorkoutService _workoutService = new WorkoutService();
        private readonly ExerciseLogService _logService = new ExerciseLogService();

        public DashboardPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var user = SessionManager.LoggedInUser;
                welcomeLabel.Text = $"Welcome, {user.Username}!";

                var workouts = await _workoutService.GetAllWorkoutsAsync();
                var userWorkouts = workouts.Where(w => w.UserId == user.Id).ToList();
                workoutCountLabel.Text = $"💪 {userWorkouts.Count()}";

                var logs = await _logService.GetExerciseLogsAsync();
                var userLogs = logs.Where(l => l.UserId == user.Id).ToList();
                logCountLabel.Text = $"📒 {userLogs.Count}";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to load stats: {ex.Message}", "OK");
            }
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            SessionManager.LoggedInUser = null;
            await Navigation.PushAsync(new AboutPage());
        }

        private async void GoToWorkout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutsPlans());
        }

        private async void GoToMealPlans(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DietTracker());
        }

        private async void GoToProgressTracking(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciseTrackerPage());
        }

        private async void GoToProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(SessionManager.LoggedInUser));
        }
    }
}
