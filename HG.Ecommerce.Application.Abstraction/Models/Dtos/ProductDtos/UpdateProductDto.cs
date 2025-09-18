namespace HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos
{
   public record UpdateProductDto(string ProductCode, string Name, string? ImagePath, decimal Price, int MinimumQuantity, double DiscountRate);
}
