using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessTracker.ViewModels
{
    public class ExerciseTrackerViewModel : BindableObject
    {
        public ObservableCollection<Exercise> LoggedExercises { get; set; }

        public Exercise NewExercise { get; set; }

        public ICommand LogExerciseCommand { get; }

        public ExerciseTrackerViewModel()
        {
            LoggedExercises = new ObservableCollection<Exercise>();
            NewExercise = new Exercise();
            LogExerciseCommand = new Command(LogExercise);
        }

        private void LogExercise()
        {
            if (!string.IsNullOrWhiteSpace(NewExercise.Name) &&
                !string.IsNullOrWhiteSpace(NewExercise.Reps) &&
                !string.IsNullOrWhiteSpace(NewExercise.Sets) &&
                !string.IsNullOrWhiteSpace(NewExercise.Time))
            {
                LoggedExercises.Add(new Exercise
                {
                    Name = NewExercise.Name,
                    Reps = NewExercise.Reps,
                    Sets = NewExercise.Sets,
                    Time = NewExercise.Time
                });

                // Clear inputs after logging
                NewExercise = new Exercise();
                OnPropertyChanged(nameof(NewExercise));
            }
        }
    }

    public class Exercise
    {
        public string Name { get; set; }
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string Time { get; set; }
        public string DisplayInfo => $"Reps: {Reps}, Sets: {Sets}, Time: {Time} min";
    }
}
