using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using TechnicalExercice.Infrastructure.Context;

namespace TechnicalExercice.Infrastructure.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly MyDbContext _context;
        public ShoppingService(MyDbContext context)
        {
            _context = context;
        }


        public ShoppingBasket AddProductToBasket(Guid clientId, Guid? basketId, Product product, int quantity)
        {
            ShoppingBasket? basket = null;

            if (!basketId.HasValue)
            {
                // No basketId provided -> create a new basket for the client
                basket = new ShoppingBasket($"shoppingBasket- {clientId}", DateOnly.FromDateTime(DateTime.UtcNow), clientId);
            }
            else
            {
                var bid = basketId.Value;
                basket = _context.ShoppingBaskets.FirstOrDefault(b => b.Id == bid) ?? new ShoppingBasket($"shoppingBasket- {clientId}", DateOnly.FromDateTime(DateTime.UtcNow), clientId);
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new Exception("Client not found");

            if (client.ShoppingBaskets.FirstOrDefault(b => b.Id == basket!.Id) == null)
            {
                basket = new ShoppingBasket($"shoppingBasket- {client.Name}", DateOnly.FromDateTime(DateTime.UtcNow), clientId);
                client.CreateBasket(basket);
                _context.ShoppingBaskets.Add(basket);

            }

            if (basket != null && client != null && basket.ClientId == clientId && quantity == 1)
            {
                basket.AddItem(product, client.GetType());
            }
            else if (basket != null && client != null && basket.ClientId == clientId && quantity > 1)
            {
                for (int i = 0; i < quantity; i++)
                {
                    basket.AddItem(product, client.GetType());
                    Console.WriteLine($"{basket.Name} : Added {i + 1} of {quantity} items {product.Name} to the basket.");
                }
            }
            else
            {
                throw new Exception("Basket does not belong to the specified client or client not found");
            }

            _context.SaveChanges();

            return basket!;

        }

        public decimal CalculateTotalPrice(Guid basketId, Guid clientId)
        {
            var basket = _context.ShoppingBaskets.FirstOrDefault(b => b.Id == basketId) ?? throw new Exception("Basket not found");
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new Exception("Client not found");
            if (basket.ClientId == client.Id)
            {
                return basket.GetTotalPrice(client.GetType());
            }
            else
            {
                throw new Exception("Basket does not belong to the specified client");
            }
        }

        public ShoppingBasket GetBasketById(Guid basketId)
        {
            var basket = _context.ShoppingBaskets.FirstOrDefault(b => b.GetId() == basketId) ?? throw new Exception("Basket not found");
            return basket;
        }

        public void RemoveProductFromBasket(Guid clientId, Guid basketId, Guid productId)
        {
            var basket = _context.ShoppingBaskets.FirstOrDefault(b => b.Id == basketId) ?? throw new Exception("Basket not found");
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new Exception("Client not found");
            if (basket.ClientId == clientId)
            {
                basket.RemoveItem(productId, client.GetType());
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Basket does not belong to the specified client");
            }
        }
    }
}