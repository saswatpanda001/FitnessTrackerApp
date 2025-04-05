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
    public partial class UserLoginPage : ContentPage
    {

        private UserService _userService;

        public UserLoginPage()
        {
            InitializeComponent();
            _userService = new UserService();
        }


      
        private async void OnSignUpTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage());
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Get values from the Entry fields
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            try
            {
                var users = await _userService.GetAllUsersAsync();
                var matchedUser = users?.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

                if (matchedUser != null)
                {
                    SessionManager.LoggedInUser = matchedUser;
                    // Successful login
                    await DisplayAlert("Success", $"Welcome, {matchedUser.Username}!", "OK");

                    // Navigate to next page (e.g., HomePage)
                    await Navigation.PushAsync(new DashboardPage());
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Login error: {ex.Message}", "OK");
            }
        }


    }
}