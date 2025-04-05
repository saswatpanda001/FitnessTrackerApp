using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitnessTracker.Models;
using FitnessTracker.Services;
using FitnessTracker.Service;

namespace FitnessTracker.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly UserService _userService = new UserService();
        private User _currentUser;

        public ProfilePage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            NavigationPage.SetHasBackButton(this, false);
        }
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUserData();
        }

        private void LoadUserData()
        {
            usernameEntry.Text = _currentUser.Username;
            emailEntry.Text = _currentUser.Email;
            fullNameEntry.Text = _currentUser.FullName;
            ageEntry.Text = _currentUser.Age?.ToString();
            weightEntry.Text = _currentUser.Weight?.ToString();
            heightEntry.Text = _currentUser.Height?.ToString();
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            // Trim input
            string newUsername = usernameEntry.Text?.Trim();
            string newEmail = emailEntry.Text?.Trim();
            string fullName = fullNameEntry.Text?.Trim();
            string ageText = ageEntry.Text?.Trim();
            string weightText = weightEntry.Text?.Trim();
            string heightText = heightEntry.Text?.Trim();

            // Basic validation
            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newEmail))
            {
                await DisplayAlert("Error", "Username and Email cannot be empty.", "OK");
                return;
            }

            var allUsers = await _userService.GetAllUsersAsync();
            if (allUsers == null)
            {
                await DisplayAlert("Error", "Unable to fetch users.", "OK");
                return;
            }

            // Check for duplicate username/email (excluding current user)
            bool usernameExists = allUsers.Any(u => u.Username == newUsername && u.Id != _currentUser.Id);
            bool emailExists = allUsers.Any(u => u.Email == newEmail && u.Id != _currentUser.Id);

            if (usernameExists)
            {
                await DisplayAlert("Error", "Username already exists.", "OK");
                return;
            }
            if (emailExists)
            {
                await DisplayAlert("Error", "Email already exists.", "OK");
                return;
            }

            // Update user
            _currentUser.Username = newUsername;
            _currentUser.Email = newEmail;
            _currentUser.FullName = fullName;
            _currentUser.Age = int.TryParse(ageText, out int age) ? age : (int?)null;
            _currentUser.Weight = float.TryParse(weightText, out float weight) ? weight : (float?)null;
            _currentUser.Height = float.TryParse(heightText, out float height) ? height : (float?)null;

            bool success = await _userService.UpdateUserAsync(_currentUser.Id, _currentUser);

            if (success)
                await DisplayAlert("Success", "Profile updated successfully!", "OK");
            else
                await DisplayAlert("Error", "Failed to update profile.", "OK");
        }
    }
}
