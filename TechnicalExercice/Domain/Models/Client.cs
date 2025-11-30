using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercice.Domain.Models
{
    public abstract class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<ShoppingBasket> ShoppingBaskets { get; private set; } = [];


        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }


        public Guid GetId() => Id;
        public string GetName() => Name;

        public abstract void CreateBasket(ShoppingBasket basket);
        public abstract void DeleteBasket(Guid baskId);
        public abstract void UpdateBasket(ShoppingBasket basket);

        public new abstract ClientType GetType();
        public abstract string GetAmountToPay(Guid basketId);

        public abstract string GetBasketDetails(Guid basketId);

        public abstract void AddItemToBasket(Guid basketId, Product product, ClientType type);

        public abstract void RemoveItemFromBasket(Guid basketId, Guid productId, ClientType type);
    }
}