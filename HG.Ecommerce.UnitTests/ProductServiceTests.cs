using HG.Ecommerce.Application.Abstraction.Exceptions;
using HG.Ecommerce.Application.Services;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System.Text;
namespace HG.Ecommerce.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("localhost:7061");
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

            _service = new ProductService(_unitOfWorkMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldThrowNotFound_WhenProductNotExist()
        {
            var product = new Product { Id = 1, Name = "Laptop", Price = 1000 };
            _unitOfWorkMock.Setup(u => u.GetRepository<Product, int>().GetByIdWithSpecAsync(It.IsAny<int>(), It.IsAny<ISpecifications<Product, int>>(), false))
                .ReturnsAsync(product);

            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetProductByIdAsync(1));
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnDto_WhenProductExists()
        {
            var product = new Product { Id = 1, Name = "Laptop", Price = 1000 };
            _unitOfWorkMock.Setup(u => u.GetRepository<Product, int>().GetByIdWithSpecAsync(It.IsAny<int>(), It.IsAny<ISpecifications<Product, int>>(), false))
                .ReturnsAsync(product);

            var result = await _service.GetProductByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Name);
        }

        [Fact]
        public async Task UploadProductImage_ShouldSaveFileAndReturnUrl()
        {
            var product = new Product { Id = 1, Name = "Phone" };

            _unitOfWorkMock
                .Setup(u => u.GetRepository<Product, int>().GetByIdAsync(1))
                .ReturnsAsync(product);

            _unitOfWorkMock
                .Setup(u => u.GetRepository<Product, int>().UpdateAsync(product))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var fileContent = "fake image content";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            IFormFile file = new FormFile(stream, 0, stream.Length, "file", "test.jpg");

            var url = await _service.UploadProductImage(1, file);

            Assert.Contains("https://localhost:7061/uploads/", url);
            Assert.EndsWith(".jpg", url);
            Assert.Equal(product.ImagePath, url);
        }
    }
}
