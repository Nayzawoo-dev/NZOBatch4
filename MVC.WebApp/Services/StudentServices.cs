using MVC.WebApp.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVC.WebApp.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly HttpClient _httpClient;

        public StudentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StudentModel>> GetWalletsAsync()
        {
            var response = await _httpClient.GetAsync("api/wallet");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<StudentModel> lst = JsonConvert.DeserializeObject<List<StudentModel>>(json)!;
                return lst;
            }
            else
            {
                throw new Exception("Failed to retrieve wallets.");
            }
        }

        //public async Task<List<StudentModel>> GetStudentsAsync()
        //{
        //    var response = await _httpClient.GetAsync("https://localhost:7223/api/Student");
        //    response.EnsureSuccessStatusCode();
        //    var students = await response.Content.ReadFromJsonAsync<List<StudentModel>>();
        //    return students ?? new List<StudentModel>();
        //}



    }
}
