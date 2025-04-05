using System;
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
                new WorkoutPlan { Name = "Beginner Plan", Description = "Perfect for starters.", Image = "beginner_workout.jpg", StartTime = "06:00 AM", EndTime = "07:00 AM" },
                new WorkoutPlan { Name = "Intermediate Plan", Description = "For regular exercisers.", Image = "intermediate_workout.jpg", StartTime = "07:30 AM", EndTime = "08:30 AM" },
                new WorkoutPlan { Name = "Advanced Plan", Description = "High-intensity training.", Image = "advanced_workout.jpg", StartTime = "09:00 AM", EndTime = "10:00 AM" },
                new WorkoutPlan { Name = "Intermediate Plan", Description = "For regular exercisers.", Image = "intermediate_workout.jpg", StartTime = "07:30 AM", EndTime = "08:30 AM" },
                new WorkoutPlan { Name = "Advanced Plan", Description = "High-intensity training.", Image = "advanced_workout.jpg", StartTime = "09:00 AM", EndTime = "10:00 AM" },
            };

            AddWorkoutCommand = new Command(AddWorkout);
        }

        private void AddWorkout()
        {
            WorkoutPlans.Add(new WorkoutPlan
            {
                Name = "Custom Plan",
                Description = "Your custom workout.",
                Image = "custom_workout.jpg",
                StartTime = "08:00 AM",
                EndTime = "09:00 AM"
            });
        }
    }

    public class WorkoutPlan
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
