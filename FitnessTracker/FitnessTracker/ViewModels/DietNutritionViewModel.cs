using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessTracker.ViewModels
{
    public class MealPlan
    {
        public string MealType { get; set; }  // Breakfast, Lunch, Dinner
        public string MealDescription { get; set; }  // Meal details
        public string Image { get; set; }  // Image URL or local path
    }

    public class DietNutritionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<MealPlan> _mealPlans;
        public ObservableCollection<MealPlan> MealPlans
        {
            get => _mealPlans;
            set
            {
                _mealPlans = value;
                OnPropertyChanged(nameof(MealPlans));
            }
        }

        private string _selectedGoal;
        public string SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                LoadMealPlans(value);
                OnPropertyChanged(nameof(SelectedGoal));
            }
        }

        public DietNutritionViewModel()
        {
            MealPlans = new ObservableCollection<MealPlan>();
        }

        private void LoadMealPlans(string goal)
        {
            MealPlans.Clear();

            if (goal == "Weight Loss")
            {
                MealPlans.Add(new MealPlan { MealType = "Breakfast", MealDescription = "Oatmeal with fruits & nuts", Image = "oatmeal.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Lunch", MealDescription = "Grilled chicken with salad", Image = "chicken_salad.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Dinner", MealDescription = "Quinoa with steamed veggies", Image = "quinoa.jpg" });
            }
            else if (goal == "Muscle Gain")
            {
                MealPlans.Add(new MealPlan { MealType = "Breakfast", MealDescription = "Scrambled eggs & whole wheat toast", Image = "eggs_toast.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Lunch", MealDescription = "Brown rice & lean beef", Image = "brown_rice_beef.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Dinner", MealDescription = "Salmon & sweet potatoes", Image = "salmon_sweet_potato.jpg" });
            }
            else if (goal == "Maintenance")
            {
                MealPlans.Add(new MealPlan { MealType = "Breakfast", MealDescription = "Greek yogurt & berries", Image = "yogurt_berries.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Lunch", MealDescription = "Vegetable stir-fry with tofu", Image = "stir_fry_tofu.jpg" });
                MealPlans.Add(new MealPlan { MealType = "Dinner", MealDescription = "Grilled fish & quinoa salad", Image = "fish_quinoa.jpg" });
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
