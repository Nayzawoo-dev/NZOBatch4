using Database.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.TblProducts
                .Where(x => x.IsDelete == false)
                .Select(x => new ProductResponse
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Quantity = x.Quantity
                })
                .ToList();

            return Ok(new Response
            {
                Success = true,
                Message = "Product list retrieved successfully",
                Data = products
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateRequest request)
        {
            var product = new TblProduct
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                IsDelete = false
            };

            _context.TblProducts.Add(product);
            _context.SaveChanges();

            return Ok(new Response
            {
                Success = true,
                Message = "Product created successfully",
                Data = new ProductResponse
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = product.Quantity
                }
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ProductUpdateRequest request)
        {
            var product = _context.TblProducts
                .FirstOrDefault(x => x.Id == id && x.IsDelete == false);

            if (product == null)
            {
                return NotFound(new Response
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            if (request.ProductName != null)
                product.ProductName = request.ProductName;

            if (request.Price.HasValue)
                product.Price = request.Price;

            if (request.Quantity.HasValue)
                product.Quantity = request.Quantity;

            _context.SaveChanges();

            return Ok(new Response
            {
                Success = true,
                Message = "Product updated successfully",
                Data = new ProductResponse
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = product.Quantity
                }
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.TblProducts
                .FirstOrDefault(x => x.Id == id && x.IsDelete == false);

            if (product == null)
            {
                return NotFound(new Response
                {
                    Success = false,
                    Message = "Product not found"
                });
            }

            product.IsDelete = true;
            _context.SaveChanges();

            return Ok(new Response
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }
    }
}
