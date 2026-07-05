using MVC.WebApp.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

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

        
        public async Task<bool> CreateStudentAsync(StudentModel student)
        {
            
            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, Application.Json);

            
            var response = await _httpClient.PostAsync("api/Student", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception("Failed to create student.");
            }
        }


    }
}
