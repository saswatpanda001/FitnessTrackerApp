using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FitnessTracker.Models;
using Newtonsoft.Json;

namespace FitnessTracker.Services
{
    public class ExerciseLogService
    {
        private readonly HttpClient _client;
        private readonly string baseUrl = "http://10.0.2.2:47541/api/ExerciseLogs"; // Update as per your API URL

        public ExerciseLogService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _client = new HttpClient(handler);
        }

        // ✅ Get all exercise logs
        public async Task<List<ExerciseLog>> GetExerciseLogsAsync()
        {
            var response = await _client.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<List<ExerciseLog>>(response);
        }

        

        // ✅ Create a new exercise log
        public async Task<bool> CreateExerciseLogAsync(ExerciseLog log)
        {
            string json = JsonConvert.SerializeObject(log);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(baseUrl, content);
            return response.IsSuccessStatusCode;
        }

       

        // ✅ Delete an exercise log
        public async Task<bool> DeleteExerciseLogAsync(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }


        // ✅ Update an existing exercise log
        public async Task<bool> UpdateExerciseLogAsync(ExerciseLog log)
        {
            string json = JsonConvert.SerializeObject(log);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{baseUrl}/{log.Id}", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Get a specific exercise log by ID
        public async Task<ExerciseLog> GetExerciseLogByIdAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExerciseLog>(json);
            }

            return null;
        }


    }
}
