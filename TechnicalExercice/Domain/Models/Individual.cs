using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnicalExercice.Domain.Models
{
    public class Individual : Client
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public ClientType TypeI { get; private set; }
        public List<ShoppingBasket> Baskets { get; private set; } = [];
        public Currency Currency { get; private set; } = Currency.EUR;
        public decimal Revenue { get; private set; }
        public Individual(string firstName, string lastName) : base($"{firstName} {lastName}")
        {
            FirstName = firstName;
            LastName = lastName;
            TypeI = ClientType.Individual;
        }


        public override ClientType GetType()
        {
            return TypeI;
        }

        public void ChangeCurrency(Currency newCurrency)
        {
            Currency = newCurrency;
            Console.WriteLine($"Currency changed to {Currency} for Individual customer.");
        }

        public string GetRevenue() => $"{Revenue.ToString()} EUR";

        public void SetRevenue(decimal revenue) => Revenue = revenue;

        public override string GetAmountToPay(Guid basketId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }
            return basket.GetTotalPrice(TypeI).ToString();
        }

        public override string GetBasketDetails(Guid basketId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }
            return $"Basket ID: {basket.GetId()}, Name: {basket.GetName()}, Total Price: {basket.GetTotalPrice(TypeI)}";
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

            basket.AddItem(product, TypeI);

            if (!Baskets.Contains(basket))
            {
                Baskets.Add(basket);
            }
            Console.WriteLine($"Item {product.GetName()} added to basket {basketId} for Individual customer.");
        }

        public override void RemoveItemFromBasket(Guid basketId, Guid productId, ClientType type)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == basketId);
            if (basket == null)
            {
                throw new ArgumentException("Basket not found");
            }
            basket.RemoveItem(productId, TypeI);
            Console.WriteLine($"Item with ID {productId} removed from basket {basketId} for Individual customer.");
        }

        public override void CreateBasket(ShoppingBasket basket)
        {
            if(Baskets.Any(b => b.GetId() == basket.GetId()))
            {
                throw new ArgumentException("Basket with the same ID already exists");
            }
            else
            {
                Baskets.Add(basket);
            }
            Baskets.Add(basket);
        }

        public override void DeleteBasket(Guid baskId)
        {
            var basket = Baskets.FirstOrDefault(b => b.GetId() == baskId);
            if (basket != null)
            {
                Baskets.Remove(basket);
            }
        }

        public override void UpdateBasket(ShoppingBasket basket)
        {
            var existingBasket = Baskets.FirstOrDefault(b => b.GetId() == basket.GetId());
            if (existingBasket != null)
            {
                Baskets.Remove(existingBasket);
                Baskets.Add(basket);
            }
        }
    }
}
