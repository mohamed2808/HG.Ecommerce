using HG.Ecommerce.Application.Abstraction.Contracts;
using HG.Ecommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace HG.Ecommerce.Application.DependancyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServicesManager, ServiceManager>();
            return services;
        }
    }
}
