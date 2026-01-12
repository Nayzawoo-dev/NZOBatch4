using BlogApi.Model;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IBlogServices _services;

        public BlogController(IBlogServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> BlogCreate([FromBody] CreateRequestModel model)
        {
            var result = await _services.CreateAsync(model);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> BlogList()
        {
            var result = await _services.ReadAsync();
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> BlogList(int id)
        {
            var result = await _services.ReadAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPatch("{id?}")]

        public async Task<IActionResult> UpdateAsync(int? id, [FromBody] UpdateRequest model)
        {
            var result = await _services.UpdateAsync(id,model);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id?}")]

        public async Task<IActionResult> DeleteAsync(int? id)
        {
            var result = await _services.DeleteAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}