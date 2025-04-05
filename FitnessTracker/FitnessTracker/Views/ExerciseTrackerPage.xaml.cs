using FitnessTracker.Models;
using FitnessTracker.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessTracker.Views
{
    public partial class ExerciseTrackerPage : ContentPage
    {
        private ExerciseLogService _service;
        private int _currentUserId = SessionManager.LoggedInUser.Id;

        public ObservableCollection<ExerciseLog> LoggedExercises { get; set; } = new ObservableCollection<ExerciseLog>();
        public ObservableCollection<ExerciseLog> FilteredExercises { get; set; } = new ObservableCollection<ExerciseLog>();
        public ExerciseLog NewExercise { get; set; } = new ExerciseLog();

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterExercises();
            }
        }
        private string _searchText;

        public ExerciseTrackerPage()
        {
            InitializeComponent();
            _service = new ExerciseLogService();

            BindingContext = this;

            LoadExerciseLogs();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

        private async void LoadExerciseLogs()
        {
            try
            {
                var allLogs = await _service.GetExerciseLogsAsync();
                var userLogs = allLogs
                .Where(e => e.UserId == _currentUserId)
                .OrderByDescending(e => e.CreatedAt)
                .ToList();

                LoggedExercises.Clear();
                FilteredExercises.Clear();

                foreach (var log in userLogs)
                {
                    LoggedExercises.Add(log);
                    FilteredExercises.Add(log);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to load logs: " + ex.Message, "OK");
            }
        }

        public Command LogExerciseCommand => new Command(async () => await LogExerciseAsync());
        public Command SearchCommand => new Command(FilterExercises);

        private async Task LogExerciseAsync()
        {
            if (string.IsNullOrWhiteSpace(NewExercise.ExerciseName))
            {
                await DisplayAlert("Validation", "Please enter exercise name.", "OK");
                return;
            }

            NewExercise.UserId = _currentUserId;

            bool success = await _service.CreateExerciseLogAsync(NewExercise);
            if (success)
            {
                await DisplayAlert("Success", "Exercise logged successfully.", "OK");
                NewExercise = new ExerciseLog(); // Reset form
                OnPropertyChanged(nameof(NewExercise));
                LoadExerciseLogs();
            }
            else
            {
                await DisplayAlert("Error", "Failed to log exercise.", "OK");
            }
        }

        private void FilterExercises()
        {
            FilteredExercises.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? LoggedExercises
                : new ObservableCollection<ExerciseLog>(LoggedExercises
                    .Where(e => e.ExerciseName.ToLower().Contains(SearchText.ToLower())));

            foreach (var item in filtered)
                FilteredExercises.Add(item);
        }

        // Call this in XAML with TapGestureRecognizer (if needed)
        public async void OnDeleteTapped(object sender, EventArgs e)
        {
            if (sender is Image image && image.BindingContext is ExerciseLog log)
            {
                bool confirm = await DisplayAlert("Confirm", $"Delete {log.ExerciseName}?", "Yes", "No");
                if (confirm)
                {
                    bool deleted = await _service.DeleteExerciseLogAsync(log.Id);
                    if (deleted)
                    {
                        LoggedExercises.Remove(log);
                        FilteredExercises.Remove(log);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete log.", "OK");
                    }
                }
            }
        }
    }
}
