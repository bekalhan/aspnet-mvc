using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WP.Models;

namespace WP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        readonly private WebAppContext _context;
        public ProductApiController(WebAppContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var getAllProducts = await _context.Products.ToListAsync();
            if (getAllProducts is null)
            {
                return NoContent();
            }
            return getAllProducts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var getSpesificProduct = await _context.Products.FirstOrDefaultAsync(x => x.ID == id);
            if (getSpesificProduct is null)
            {
                return NoContent();
            }
            return getSpesificProduct;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var updateProduct = _context.Products.FirstOrDefault(x => x.ID == id);
            if (updateProduct is null)
            {
                return NotFound();
            }
            updateProduct.Title = product.Title;
            updateProduct.Brand = product.Brand;
            updateProduct.Price = product.Price;
            updateProduct.Color = product.Color;
            updateProduct.Description = product.Description;
            _context.Update(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteProduct = _context.Products.FirstOrDefault(x => x.ID == id);
            if (deleteProduct is null)
            {
                return NotFound();
            }
            if (_context.Comments.Any(x => x.ProductId == id))
            {
                return NotFound("Ürüne ait yorumlar var.");
            }
            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();
            return Ok();
        }






















    }
}
