using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercice.API
{
    public interface IEndpoint
    {
        void Map(IEndpointRouteBuilder app);
    }
}