using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OefenTestRouting.Models;
using OefenTestRouting.Services;

namespace OefenTestRouting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productsService)
        {
            _productService = productsService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return BadRequest();
            }

            return Ok(product);
        }



        

    }
}
