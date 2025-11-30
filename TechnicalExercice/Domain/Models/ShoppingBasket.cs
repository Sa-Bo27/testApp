using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TechnicalExercice.Domain.Models
{
    public class ShoppingBasket
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateOnly CreatedAt { get; private set; } = new DateOnly();

        public List<Product> Products { get; private set; } = [];

        public decimal TotalPrice { get; private set; }

        public Guid ClientId { get; private set; }


        public ShoppingBasket(string name, DateOnly createdAt, Guid clientId)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = createdAt;
            TotalPrice = 0;
            ClientId = clientId;
        }

        public ShoppingBasket() { }

        public List<Product> GetItems()
        {
            return Products;
        }

        public Guid GetId() => Id;
        public string GetName() => Name;

        public void AddItem(Product item, ClientType type)
        {

            Products.Add(item);
            TotalPrice += item.GetPrice(type);
        }

        public void RemoveItem(Guid id, ClientType type)
        {
            var itemToRemove = Products.FirstOrDefault(i => i.GetId() == id);
            if (itemToRemove != null)
            {
                Products.Remove(itemToRemove);
                TotalPrice -= itemToRemove.GetPrice(type);
            }
        }

        public decimal GetTotalPrice(ClientType type)
        {
            return TotalPrice;
        }
    }
}
