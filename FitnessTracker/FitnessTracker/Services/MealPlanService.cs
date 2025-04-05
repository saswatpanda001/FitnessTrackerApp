using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FitnessTracker.Models; // Adjust this namespace as per your actual model location

namespace FitnessTracker.Service
{
    public class MealPlanService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "http://10.0.2.2:47541/api/MealPlans"; // Change to your API URL

        public MealPlanService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);
        }

        public async Task<List<MealPlan>> GetMealPlansAsync()
        {
            var response = await _client.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<List<MealPlan>>(response);
        }

        public async Task<MealPlan> GetMealPlanByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MealPlan>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching meal plan: {ex.Message}");
            }

            return null;
        }

        public async Task<bool> CreateMealPlanAsync(MealPlan mealPlan)
        {
            var json = JsonConvert.SerializeObject(mealPlan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMealPlanAsync(MealPlan mealPlan)
        {
            try
            {
                var json = JsonConvert.SerializeObject(mealPlan);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{baseUrl}/{mealPlan.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating meal plan: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteMealPlanAsync(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
