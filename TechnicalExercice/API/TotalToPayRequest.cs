using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercice.API
{
    public record TotalToPayRequest(Guid ClientId, Guid BasketId)
    {
        
    }
}