// See https://aka.ms/new-console-template for more information
using Blog.ConsoleApp;

Console.WriteLine("Api Testing.........");

BlogConsoleServices _services = new BlogConsoleServices();
var res = _services.CreateBlog();
Console.WriteLine(res);
Console.ReadLine();