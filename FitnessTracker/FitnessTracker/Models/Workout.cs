using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WorkoutName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
