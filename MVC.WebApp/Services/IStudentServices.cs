using MVC.WebApp.Models;

namespace MVC.WebApp.Services
{
    public interface IStudentServices
    {
        Task<List<StudentModel>> GetStudentsAsync();
    }
}