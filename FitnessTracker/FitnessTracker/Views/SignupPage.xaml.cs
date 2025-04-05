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
    public partial class SignupPage : ContentPage
    {

        private UserService _userService;

        public SignupPage()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text?.Trim();
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text;
            string confirmPassword = confirmPasswordEntry.Text;

            // Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            try
            {
                var existingUsers = await _userService.GetAllUsersAsync();

                bool usernameExists = existingUsers.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                bool emailExists = existingUsers.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (usernameExists)
                {
                    await DisplayAlert("Error", "Username already taken.", "OK");
                    return;
                }

                if (emailExists)
                {
                    await DisplayAlert("Error", "Email already registered.", "OK");
                    return;
                }

                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = password
                };

                bool success = await _userService.CreateUserAsync(newUser);

                if (success)
                {
                    await DisplayAlert("Success", "Account created successfully!", "OK");
                    await Navigation.PushAsync(new UserLoginPage());
                }
                else
                {
                    await DisplayAlert("Error", "Failed to create account. Try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Signup failed: {ex.Message}", "OK");
            }
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLoginPage());
        }



    }
}