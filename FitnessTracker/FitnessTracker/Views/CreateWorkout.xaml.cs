using FitnessTracker.Models;
using FitnessTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Views
{
    public partial class CreateWorkout : ContentPage
    {
        private readonly WorkoutService _workoutService = new WorkoutService();

        public CreateWorkout()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

        private async void OnSaveWorkoutClicked(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(workoutNameEntry.Text) || string.IsNullOrWhiteSpace(workoutDescriptionEditor.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Create Workout object
            var workout = new Workout
            {
                UserId = SessionManager.LoggedInUser.Id,
                WorkoutName = workoutNameEntry.Text.Trim(),
                Description = workoutDescriptionEditor.Text.Trim(),
                StartTime = DateTime.Today.Add(startTimePicker.Time),
                EndTime = DateTime.Today.Add(endTimePicker.Time)
            };

            bool isSaved = await _workoutService.CreateWorkoutAsync(workout);

            if (isSaved)
            {
                await DisplayAlert("Success", "Workout saved successfully!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to save workout.", "OK");
            }
        }
    }
}
