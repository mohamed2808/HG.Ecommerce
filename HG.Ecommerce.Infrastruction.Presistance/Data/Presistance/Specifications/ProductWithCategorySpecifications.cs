using HG.Ecommerce.Core.Entites;
namespace HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Specifications
{
   public class ProductWithCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCategorySpecifications() : base()
        {
            AddIncludes();
        }
        public ProductWithCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        private protected override void AddIncludes()
        {
            Includes.Add(product => product.Category!);
        }
    }
}
