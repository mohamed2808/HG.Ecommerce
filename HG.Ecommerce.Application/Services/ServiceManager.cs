using HG.Ecommerce.Application.Abstraction.Contracts;
using HG.Ecommerce.Core.Contracts;
using Microsoft.AspNetCore.Http;
namespace HG.Ecommerce.Application.Services
{
    class ServiceManager : IServicesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IProductService> _productService;
        public ServiceManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,httpContextAccessor));
        }
        public IProductService ProductService
        => _productService.Value;
    }
}
