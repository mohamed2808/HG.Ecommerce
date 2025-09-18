using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace HG.Ecommerce.Infrastruction.Presistance.Dependency_Injection
{
    public static class ConnectionStringServices
    {
        public static IServiceCollection GetConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
