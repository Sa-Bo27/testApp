using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.API
{
    public record AddProductRequest(Guid ClientId, Guid? BasketId, int Quantity, string ProductName, int TypeProduct)
    {

    }
}