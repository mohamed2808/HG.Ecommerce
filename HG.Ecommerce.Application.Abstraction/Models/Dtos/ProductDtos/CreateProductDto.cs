namespace HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos
{
   public record CreateProductDto(int CategoryId, string CategoryName, string ProductCode, string Name, string? ImagePath, decimal Price, int MinimumQuantity, double DiscountRate);
}
