using BlogApi.Model;
using Databased.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly AppDbContext _context;
        public BlogServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateResponseModel> CreateAsync([FromBody] CreateRequestModel model)
        {
            TblBlog blog = new TblBlog
            {
                Caption = model.Caption,
                Date = DateTime.Now,
            };
            await _context.TblBlogs.AddAsync(blog);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Post Create Successful" : "Post Create Failed";
            var response = new CreateResponseModel
            {
                Success = result > 0,
                Message = message
            };
            return response;
        }

        public async Task<ReadResponseModelByList> ReadAsync()
        {
            var list = await _context.TblBlogs.ToListAsync();
            var model = new ReadResponseModelByList
            {
                Success = list.Count > 0,
                Message = list.Count > 0 ? "Data List" : "No Data Found",
                Data = list
            };

            return model;
        }

        public async Task<ReadResponseModelById> ReadAsync(int id)
        {
            var list = await _context.TblBlogs.FirstOrDefaultAsync(x => x.Id == id);
            var model = new ReadResponseModelById
            {
                Success = list is not null,
                Message = list is not null ? "Data Found" : "Data Not Found",
                Id = list.Id,
                Caption = list.Caption,
                Date = DateTime.Now,
            };

            return model;
        }

        public async Task<UpdateResponse> UpdateAsync(int? id, [FromBody] UpdateRequest model)
        {
            UpdateResponse response;
            var blog = await _context.TblBlogs.FirstOrDefaultAsync(x => x.Id == id);

            if (blog is not null)
            {
                blog.Caption = string.IsNullOrEmpty(model.Caption) ? blog.Caption : model.Caption;
                var result = await _context.SaveChangesAsync();
                response = new UpdateResponse
                {
                    Success = result > 0,
                    Message = result > 0 ? "Update Successful" : "No Data To Update"
                };
                goto final;
            }

            response = new UpdateResponse
            {
                Success = false,
                Message = id is null ? "Id is required" : "No Data Found"
            };

        final:
            return response;

        }

        public async Task<UpdateResponse> DeleteAsync(int? id)
        {
            UpdateResponse response;
            var blog = await _context.TblBlogs.FirstOrDefaultAsync(x => x.Id == id);

            if (blog is not null)
            {
                _context.TblBlogs.Remove(blog);
                var result = await _context.SaveChangesAsync();
                response = new UpdateResponse
                {
                    Success = result > 0,
                    Message = result > 0 ? "Delete Successful" : "No Data To Delete"
                };
                goto final;
            }

            response = new UpdateResponse
            {
                Success = false,
                Message = id is null ? "Id is required" : "No Data Found"
            };

        final:
            return response;

        }

    }
}