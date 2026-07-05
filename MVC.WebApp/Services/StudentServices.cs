using MVC.WebApp.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVC.WebApp.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StudentServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiUrl")!);
        }

        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync("api/Student");

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
