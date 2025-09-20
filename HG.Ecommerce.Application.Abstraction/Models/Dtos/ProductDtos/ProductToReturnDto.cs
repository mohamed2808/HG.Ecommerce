namespace HG.Ecommerce.Application.Abstraction.Models.Dtos.ProductDtos
{
   public record ProductToReturnDto(int Id, string Name, string Description, decimal Price, string? ImagePath, int? CategoryId,string CategoryName);
}
