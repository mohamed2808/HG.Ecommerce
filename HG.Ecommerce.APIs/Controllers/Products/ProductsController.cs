using HG.Ecommerce.APIs.Controllers.Base;
using HG.Ecommerce.Application.Abstraction.Contracts;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LinkDev.Talabat.APIs.Controller.Controllers.Products
{
    public class ProductsController(IServicesManager _serviceManager) : BaseAPIController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetProductsPaged([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _serviceManager.ProductService.GetProductsPagedAsync(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var productId = await _serviceManager.ProductService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, null);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto productDto)
        {
            await _serviceManager.ProductService.UpdateProductAsynce(productDto);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _serviceManager.ProductService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpPost("{productId}/uploadImage")]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            var result = await _serviceManager.ProductService.UploadProductImage(productId, file);
            return Ok(result);
        }
    }
}
