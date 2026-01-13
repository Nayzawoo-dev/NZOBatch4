using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MonthsController : ControllerBase
{
    private readonly IMonthService _monthService;

    public MonthsController(IMonthService monthService)
    {
        _monthService = monthService;
    }

    // GET api/months/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _monthService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET api/months/search?keyword=January
    [HttpGet("search/{keyword}")]
    public async Task<IActionResult> Search(string keyword)
    {
        var result = await _monthService.SearchByMonthAsync(keyword);
        return Ok(result);
    }
}
