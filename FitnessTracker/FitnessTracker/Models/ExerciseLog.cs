using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Models
{
    public class ExerciseLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ExerciseName { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public int TimeInMinutes { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
