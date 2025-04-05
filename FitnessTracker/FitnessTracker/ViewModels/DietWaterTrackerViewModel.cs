using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;


namespace FitnessTracker.ViewModels
{
    public class DietWaterTrackerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Meals { get; set; }
        private int _waterIntake;
        private const int GoalWaterIntake = 8; // 8 glasses per day

        public int WaterIntake
        {
            get => _waterIntake;
            set
            {
                _waterIntake = value;
                OnPropertyChanged(nameof(WaterIntake));
                OnPropertyChanged(nameof(WaterProgress));
            }
        }

        public double WaterProgress => (double)WaterIntake / GoalWaterIntake;

        public ICommand AddMealCommand { get; }
        public ICommand AddWaterCommand { get; }

        public DietWaterTrackerViewModel()
        {
            Meals = new ObservableCollection<string>();
            AddMealCommand = new Command<string>(AddMeal);
            AddWaterCommand = new Command(AddWater);
        }

        private void AddMeal(string meal)
        {
            if (!string.IsNullOrWhiteSpace(meal))
                Meals.Add(meal);
        }

        private void AddWater()
        {
            if (WaterIntake < GoalWaterIntake)
                WaterIntake++;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
