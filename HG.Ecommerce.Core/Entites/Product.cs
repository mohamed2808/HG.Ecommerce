using HG.Ecommerce.Core.Common;
namespace HG.Ecommerce.Core.Entites
{
    public class Product : BaseAduitableEntity<int>
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string ProductCode { get; set; } = null!; 
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImagePath { get; set; }  
        public decimal Price { get; set; }
        public int MinimumQuantity { get; set; }
        public double DiscountRate { get; set; }
    }
}
