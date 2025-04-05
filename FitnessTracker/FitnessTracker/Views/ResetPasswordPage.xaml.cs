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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {

        private readonly UserService _userService = new UserService();

        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim();
            string newPassword = newPasswordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            if (newPassword != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }


            var users = await _userService.GetAllUsersAsync();
            var existingUser = users?.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (existingUser == null)
            {
                await DisplayAlert("Error", "Email not found.", "OK");
                return;
            }

            
            existingUser.PasswordHash = newPassword;

            bool isUpdated = await _userService.UpdateUserAsync(existingUser.Id, existingUser);

            if (isUpdated)
            {
                await DisplayAlert("Success", "Password reset successfully.", "OK");
                await Navigation.PushAsync(new UserLoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Something went wrong. Please try again.", "OK");
            }
        }

        private async void OnBackToLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }


    }
}