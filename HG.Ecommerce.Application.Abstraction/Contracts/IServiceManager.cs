using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HG.Ecommerce.Application.Abstraction.Contracts
{
    public interface IServicesManager
    {
        public IProductService ProductService { get; }
    }
}
