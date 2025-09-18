using HG.Ecommerce.Application.Abstraction.Contracts;
using HG.Ecommerce.Core.Contracts;
namespace HG.Ecommerce.Application.Services
{
    class ServiceManager : IServicesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork));
        }
        public IProductService ProductService
        => _productService.Value;
    }
}
