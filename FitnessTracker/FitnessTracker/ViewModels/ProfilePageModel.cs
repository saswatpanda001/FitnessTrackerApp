using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessTracker.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _email;
        private int _age;
        private string _profileImage;
        private int _workoutsCompleted;
        private int _caloriesBurned;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public int Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }

        public string ProfileImage
        {
            get => _profileImage;
            set { _profileImage = value; OnPropertyChanged(nameof(ProfileImage)); }
        }

        public int WorkoutsCompleted
        {
            get => _workoutsCompleted;
            set { _workoutsCompleted = value; OnPropertyChanged(nameof(WorkoutsCompleted)); }
        }

        public int CaloriesBurned
        {
            get => _caloriesBurned;
            set { _caloriesBurned = value; OnPropertyChanged(nameof(CaloriesBurned)); }
        }

        public ICommand ChangeProfilePictureCommand { get; }

        public ProfileViewModel()
        {
            // Sample data
            Name = "John Doe";
            Email = "john.doe@example.com";
            Age = 25;
            ProfileImage = "default_profile.png";
            WorkoutsCompleted = 120;
            CaloriesBurned = 45000;

            ChangeProfilePictureCommand = new Command(ChangeProfilePicture);
        }

        private async void ChangeProfilePicture()
        {
            // Simulating image picker (implement using Media Plugin or File Picker)
            ProfileImage = "new_profile.png";  // Replace with actual picked image path
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
