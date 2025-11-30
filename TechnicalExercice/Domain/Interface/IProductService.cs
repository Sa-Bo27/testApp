using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Domain.Interface
{
    public interface IProductService
    {
        decimal GetPrice(Guid productId, ClientType clientType);
        Product GetProductByName(string productName, TypeProduct typeProduct);

    }
}