using System.Net.Http.Json;
using HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace HG.Ecommerce.IntegrationTests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<EcommerceDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<EcommerceDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task Create_And_Get_Product()
        {
            var createDto = new CreateProductDto(1, "Test Laptop","P01","Labtop",null,200,5,20);

            var postResponse = await _client.PostAsJsonAsync("/api/products", createDto);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/products");
            getResponse.EnsureSuccessStatusCode();

            var products = await getResponse.Content.ReadFromJsonAsync<List<ProductToReturnDto>>();

            Assert.NotNull(products);
            Assert.Single(products);
            Assert.Equal("Test Laptop", products[0].Name);
        }
    }
}
