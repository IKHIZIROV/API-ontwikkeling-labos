using Labo03_Routing.Models;
using Labo03_Routing.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labo03_Routing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET /api/products

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productsService.GetAllProducts();
            return Ok(products);
        }

        // GET /api/products/{id:int}/details

        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // GET /api/products/search?name={name}

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProductsByName([FromQuery(Name = "name")] string name)
        {
            var products = await _productsService.SearchProductsByName(name);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET /api/products/category/{categoryName:string}/price/{minPrice:decimal}-{maxPrice:decimal}

        [HttpGet("category/{categoryName:string}/price/{minPrice:decimal}-{maxPrice:decimal}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryAndPrice(
            string categoryName, decimal minPrice, decimal maxPrice)
        {
            var products = await _productsService.GetProductsByCategoryAndPrice(categoryName, minPrice, maxPrice);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        // POST /api/products

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            await _productsService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT /api/products/{id:int}

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var updatedProduct = await _productsService.UpdateProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        // PUT /api/products/{id:int}/discount/{percentage:int}

        [HttpPut("{id:int}/discount/{percentage:int}")]
        public async Task<ActionResult<Product>> ApplyDiscountToProduct(int id, int percentage)
        {
            var product = await _productsService.ApplyDiscountToProduct(id, percentage);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // DELETE /api/products/{id:int}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productsService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productsService.DeleteProduct(id);
            return NoContent();
        }

        // DELETE /api/products/delete/multiple?ids=1,2,3

        [HttpDelete("delete/multiple")]
        public async Task<ActionResult> DeleteMultipleProducts([FromQuery] string ids)
        {
            List<int> idList = ids.Split(',').Select(int.Parse).ToList();
            await _productsService.DeleteMultipleProducts(idList);
            return NoContent();
        }
    }
}
