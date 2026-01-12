using Azure.Core;
using BlogApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ConsoleApp
{
    public class BlogConsoleServices
    {
        private readonly string domainUrl;
        private readonly string productEndpoint;

        public BlogConsoleServices()
        {
            domainUrl = "https://localhost:7268";
            productEndpoint = $"{domainUrl}/api/Blog";
        }

        public async Task ReadAsync()
        {
            Console.Write("Enter Unique Id : ");
            string id = Console.ReadLine()!;

            string endpoint = $"{productEndpoint}";

            RestClient client = new RestClient();
            RestRequest request = new RestRequest(endpoint, Method.Get);
            RestResponse result = await client.ExecuteAsync(request);
            var jsonData = result.Content!;
            Console.WriteLine(jsonData);
        }

        public async Task CreateBlog()
        {
            Console.Write("Write Caption : ");
            string caption = Console.ReadLine()!;

            CreateRequestModel model = new CreateRequestModel
            {
                Caption = caption,
                Date = DateTime.Now,
            };

            string endpoint = $"{productEndpoint}";
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(model);
            RestResponse response = await client.ExecuteAsync(request);
            var jsonData = response.Content!;
            Console.WriteLine(jsonData);
        }


    }

}
