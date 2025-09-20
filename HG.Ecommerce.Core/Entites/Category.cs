using HG.Ecommerce.Core.Common;
namespace HG.Ecommerce.Core.Entites
{
    public class Category : BaseAduitableEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
