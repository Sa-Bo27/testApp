using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalExercice.Application;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.API
{
    public class ClientEndPoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/client");

            group.MapGet("", (GetClients getClients) => {
                var result = getClients.Execute();
                return Results.Ok(result);
            });

            group.MapGet("/{clientId}/{basketId}/total-to-pay", async (Guid clientId, Guid basketId, CalculateTotalBasket calculateTotalBasket) =>
            {
                try
                {
                    var result = await calculateTotalBasket.Execute(clientId, basketId);
                    return Results.Ok($"{result} {Currency.EUR}");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/create", ([FromBody] CreateRequest request, CreateClient createClient) =>
            {
                try
                {
                        // Implementation for creating a client
                    if (!string.IsNullOrEmpty(request.CompanyName))
                    {
                        // Create Professional client
                        createClient.Execute("", "", ClientType.Professional, request.CompanyName, request.Revenue);
                    }
                    else if (!string.IsNullOrEmpty(request.FirstName) && !string.IsNullOrEmpty(request.LastName))
                    {
                        // Create Individual client
                        createClient.Execute(request.FirstName, request.LastName, ClientType.Individual, null, null);
                    }
                    return Results.Ok("Client created successfully");
                }
                catch (Exception ex)
                {
                    
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/add-product", ([FromBody] AddProductRequest request, AddProductToBasket addProductToBasket) =>
            {
                try
                {
                    var result = addProductToBasket.Execute(request.ClientId, request.BasketId, request.ProductName, request.TypeProduct, request.Quantity);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
        }
    }
}