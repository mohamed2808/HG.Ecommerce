using HG.Ecommerce.Application.Abstraction.Contracts;
using HG.Ecommerce.Application.Abstraction.Exceptions;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.CategoryDtos;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Core.Entites;
using HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Specifications;
using Mapster;
using Microsoft.AspNetCore.Http;
namespace HG.Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly string _uploadPath;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProductsAsync()
        {
            var specs = new ProductWithCategorySpecifications();
            var products = await _unitOfWork.GetRepository<Product, int>()
                .GetAllWithSpecAsync(specs,withTracking: false);
            if (products == null || products.Count() == 0)
                return new List<ProductToReturnDto>();
            var productsToReturn = products.Adapt<IEnumerable<ProductToReturnDto>>();
            return productsToReturn;
        }

        public async Task<ProductToReturnDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithCategorySpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>()
                .GetByIdWithSpecAsync(id, specs, false);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            var productToReturn = product.Adapt<ProductToReturnDto>();
            return productToReturn;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category, int>().GetAllAsync();
            var categoriesToReturn = categories.Adapt<IEnumerable<CategoryDto>>();
            return categoriesToReturn;
        }

        public Task<int> CreateProductAsync(CreateProductDto productDto)
        {
            if (productDto == null)
                throw new BadRequestException(nameof(productDto));
            var product = productDto.Adapt<Product>();
            _unitOfWork.GetRepository<Product, int>().AddAsync(product);
            return _unitOfWork.CompleteAsync();
        }

        public Task UpdateProductAsynce(UpdateProductDto productDto)
        {
            if (productDto == null)
                throw new BadRequestException(nameof(productDto));
            var product = productDto.Adapt<Product>();
            _unitOfWork.GetRepository<Product, int>().UpdateAsync(product);
            return _unitOfWork.CompleteAsync();
        }

        public Task DeleteProductAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ID must be greater than zero.");
            _unitOfWork.GetRepository<Product, int>().DeleteAsync(id);
            return _unitOfWork.CompleteAsync();
        }
        public async Task<string> UploadProductImage(int productId, IFormFile file)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(productId);
            if (product == null)
                throw new NotFoundException("Product not found.");

            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");

            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                string oldImagePath = Path.Combine(_uploadPath, Path.GetFileName(product.ImagePath));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(_uploadPath, fileName);

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = $"{request?.Scheme}://{request?.Host}";
            string fileUrl = $"{baseUrl}/uploads/{fileName}";

            product.ImagePath = fileUrl;
            await _unitOfWork.GetRepository<Product, int>().UpdateAsync(product);
            await _unitOfWork.CompleteAsync();

            return fileUrl;
        }
        public Task<string> GetProductImage(int productId)
        {
            var product = _unitOfWork.GetRepository<Product, int>().GetByIdAsync(productId);
            if (product == null)
                throw new NotFoundException("Product not found.");
            return Task.FromResult(product.Result!.ImagePath)!;
        }

    }
}
