using FitnessTracker.Models;
using FitnessTracker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FitnessTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietTracker : ContentPage
    {
        private List<MealPlan> _allMealPlans;

        public DietTracker()
        {
            InitializeComponent();
            LoadMealPlans();
            NavigationPage.SetHasBackButton(this, false);
        }
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }
        private async void LoadMealPlans()
        {
            var service = new MealPlanService();
            _allMealPlans = await service.GetMealPlansAsync();

            // Get unique categories and populate the Picker
            var categories = _allMealPlans.Select(m => m.Category).Distinct().ToList();
            goalPicker.ItemsSource = categories;

            // Optionally pre-select the first one
            if (categories.Any())
                goalPicker.SelectedIndex = 0;
        }

        private void OnGoalSelected(object sender, EventArgs e)
        {
            if (goalPicker.SelectedIndex == -1 || _allMealPlans == null) return;

            string selectedCategory = goalPicker.SelectedItem.ToString();

            var filteredPlans = _allMealPlans
                .Where(m => m.Category == selectedCategory)
                .ToList();

            mealPlanList.ItemsSource = filteredPlans;
        }
    }
}
