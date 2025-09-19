using HG.Ecommerce.Application.Abstraction.Models.Dtos.CategoryDtos;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos;
using Microsoft.AspNetCore.Http;
namespace HG.Ecommerce.Application.Abstraction.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync();
        Task<ProductToReturnDto> GetProductByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<int> CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsynce(UpdateProductDto productDto);
        Task DeleteProductAsync(int id);
        Task<string> UploadProductImage(int productId, IFormFile file);
        Task<string> GetProductImage(int productId);

    }
}
