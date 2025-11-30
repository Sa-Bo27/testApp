using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnicalExercice.Domain.Models
{
    public class Professional : Client
    {
        public string CompanyName { get; private set; } = string.Empty;

        public ClientType TypeP { get; private set; }
        public List<ShoppingBasket> Baskets { get; private set; } = [];

        public Currency Currency { get; private set; } = Currency.EUR;

        public decimal Revenue { get; private set; }
        public Professional(string name, decimal revenue) : base(name)
        {
            CompanyName = name;
            Revenue = revenue;
            if (revenue >= 10_000_000)
            {
                TypeP = ClientType.ProfessionalHighThan10M;
            }
            else
            {
                TypeP = ClientType.Professional;
            }
        }

        public override ClientType GetType()
        {
            return TypeP;
        }

        public string GetRevenue() => $"{Revenue.ToString()} EUR";

        public void SetRevenue(decimal revenue) => Revenue = revenue;

        public void ChangeCurrency(Currency newCurrency)
        {

            Currency = newCurrency;
            Console.WriteLine($"Currency changed to {Currency} for Professional customer.");
        }
        public override string GetAmountToPay(Guid basketId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);

            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }
            return basket.GetTotalPrice(ClientType.Professional).ToString();

        }

        public override string GetBasketDetails(Guid basketId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }
            return $"Basket ID: {basket.GetId()}, Name: {basket.GetName()}, Total Price: {basket.GetTotalPrice(TypeP)}";
        }

        public override void AddItemToBasket(Guid basketId, Product product, ClientType type)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                var newBasket = new ShoppingBasket($"Basket_{basketId}", DateOnly.FromDateTime(DateTime.Now), this.GetId());
                Baskets.Add(newBasket);
                basket = newBasket;
            }
            basket.AddItem(product, TypeP);

            if (!Baskets.Contains(basket))
            {
                Baskets.Add(basket);
            }
            Console.WriteLine($"Item {product.GetName()} added to basket {basketId} for Professional customer.");
        }

        public override void RemoveItemFromBasket(Guid basketId, Guid productId, ClientType type)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }

            basket.RemoveItem(productId, TypeP);
            Console.WriteLine($"Item with ID {productId} removed from basket {basketId} for Professional customer.");

        }

        public override void CreateBasket(ShoppingBasket basket)
        {
    
            if (Baskets.Any(b => b.GetId() == basket.GetId()))
            {
                throw new ArgumentException("Basket with the same ID already exists");
            }
            else
            {
                Baskets.Add(basket);
            }
        }

        public override void DeleteBasket(Guid baskId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == baskId);
            if (basket != null)
            {
                Baskets.Remove(basket);
                Console.WriteLine($"Basket with ID {baskId} deleted for Professional customer.");
            }
            else
            {
                Console.WriteLine($"Basket with ID {baskId} not found for Professional customer.");
            }
        }

        public override void UpdateBasket(ShoppingBasket basket)
        {
            var existingBasket = Baskets.FirstOrDefault(b => b.GetId() == basket.GetId());
            if (existingBasket != null)
            {
                Baskets.Remove(existingBasket);
                Baskets.Add(basket);
                Console.WriteLine($"Basket with ID {basket.GetId()} updated for Professional customer.");
            }
            else
            {
                Console.WriteLine($"Basket with ID {basket.GetId()} not found for Professional customer.");
            }
        }
    }
}

