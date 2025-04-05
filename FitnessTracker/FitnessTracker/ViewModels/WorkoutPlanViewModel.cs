using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;


namespace FitnessTracker.ViewModels
{
    public class WorkoutPlansViewModel : BindableObject
    {
        public ObservableCollection<WorkoutPlan> WorkoutPlans { get; set; }

        public ICommand AddWorkoutCommand { get; }

        public WorkoutPlansViewModel()
        {
            WorkoutPlans = new ObservableCollection<WorkoutPlan>
            {
                new WorkoutPlan { Name = "Beginner Plan", Description = "Perfect for starters.", Image = "beginner_workout.jpg" },
                new WorkoutPlan { Name = "Intermediate Plan", Description = "For regular exercisers.", Image = "intermediate_workout.jpg" },
                new WorkoutPlan { Name = "Advanced Plan", Description = "High-intensity training.", Image = "advanced_workout.jpg" }
            };

            AddWorkoutCommand = new Command(AddWorkout);
        }

        private void AddWorkout()
        {
            WorkoutPlans.Add(new WorkoutPlan { Name = "Custom Plan", Description = "Your custom workout.", Image = "custom_workout.jpg" });
        }
    }

    public class WorkoutPlan
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
