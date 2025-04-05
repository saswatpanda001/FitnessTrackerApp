using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string Category { get; set; } // e.g. "Weight Loss", "Muscle Gain"
    }

}
