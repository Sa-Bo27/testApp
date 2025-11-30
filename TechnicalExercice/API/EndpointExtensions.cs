using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace TechnicalExercice.API
{
    public static class EndpointExtensions
    {
        // Use the WebApplication (which implements IEndpointRouteBuilder) so we can access the
        // application's IServiceProvider and create endpoint instances with DI support.
        public static void MapEndpoints(this WebApplication app)
        {
            var endpointsTypes = typeof(EndpointExtensions).Assembly.GetTypes()
                .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var endpointType in endpointsTypes)
            {
                // Create endpoint instance without resolving scoped services from the root provider.
                // Endpoints should resolve scoped services per-request (via handler parameters) rather
                // than capture scoped instances at startup.
                var endpointInstance = (IEndpoint?)Activator.CreateInstance(endpointType)!;
                endpointInstance.Map(app);
            }
        }
    }
}
