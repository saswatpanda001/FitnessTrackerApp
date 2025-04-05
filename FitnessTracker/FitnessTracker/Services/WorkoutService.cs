using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FitnessTracker.Models;

namespace FitnessTracker.Services
{
    public class WorkoutService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "http://10.0.2.2:47541/api/Workouts"; // Change as per your API base URL

        public WorkoutService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);
        }

        // ✅ Get all workouts
        public async Task<List<Workout>> GetAllWorkoutsAsync()
        {
            var response = await _client.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<List<Workout>>(response);
        }



        // ✅ Create a new workout
        public async Task<bool> CreateWorkoutAsync(Workout workout)
        {
            var json = JsonConvert.SerializeObject(workout);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(baseUrl, content);
            return response.IsSuccessStatusCode;
        }



       

        // ✅ Get workout by ID
        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Workout>(json);
                }
                else
                {
                    Console.WriteLine($"Error fetching workout. Status Code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetWorkoutByIdAsync: {ex.Message}");
                return null;
            }
        }

       

        // ✅ Update workout
        public async Task<bool> UpdateWorkoutAsync(Workout workout)
        {
            try
            {
                var json = JsonConvert.SerializeObject(workout);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{baseUrl}/{workout.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateWorkoutAsync: {ex.Message}");
                return false;
            }
        }

        // ✅ Delete workout
        public async Task<bool> DeleteWorkoutAsync(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        // ✅ Get total workouts count
        public async Task<int> GetTotalWorkoutsAsync()
        {
            var workouts = await GetAllWorkoutsAsync();
            return workouts?.Count ?? 0;
        }
    }
}
