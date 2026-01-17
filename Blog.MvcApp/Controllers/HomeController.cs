using Blog.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.MvcApp.Controllers;

public class BlogResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CommentResponseModel
{
    public int Id { get; set; }
    public string CommentName { get; set; } = string.Empty;
    public string CommentDescription { get; set; } = string.Empty;
}
public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        BlogResponseModel model = new BlogResponseModel()
        {
            Id = 1,
            Name = "Nay Zaw Oo",
            Description = "I Crush You Ma Ma"
        };

        List<CommentResponseModel> comments = new List<CommentResponseModel>()
{
    new CommentResponseModel()
    {
        Id = 1,
        CommentName = "Great Post",
        CommentDescription = "Very informative and well written."
    },
    new CommentResponseModel()
    {
        Id = 2,
        CommentName = "Helpful",
        CommentDescription = "This helped me understand the topic better."
    },
    new CommentResponseModel()
    {
        Id = 3,
        CommentName = "Nice",
        CommentDescription = "Good explanation with clear examples."
    },
    new CommentResponseModel()
    {
        Id = 4,
        CommentName = "Excellent",
        CommentDescription = "Excellent breakdown of the problem."
    },
    new CommentResponseModel()
    {
        Id = 5,
        CommentName = "Thanks",
        CommentDescription = "Thanks for sharing this knowledge."
    },
    new CommentResponseModel()
    {
        Id = 6,
        CommentName = "Awesome",
        CommentDescription = "Awesome content, keep it up!"
    },
    new CommentResponseModel()
    {
        Id = 7,
        CommentName = "Clear",
        CommentDescription = "Very clear and concise explanation."
    },
    new CommentResponseModel()
    {
        Id = 8,
        CommentName = "Useful",
        CommentDescription = "This is very useful for beginners."
    },
    new CommentResponseModel()
    {
        Id = 9,
        CommentName = "Good Read",
        CommentDescription = "Enjoyed reading this article."
    },
    new CommentResponseModel()
    {
        Id = 10,
        CommentName = "Well Done",
        CommentDescription = "Well done, looking forward to more."
    }
};

        ViewData["Comments"] = comments;
        ViewBag.Comments = comments; 
        TempData["LastUpdated"] = DateTime.Now.ToString("g");

        return View(comments);
    }

    public IActionResult Privacy()
    {
        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
