using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Domain.Interface
{
    public interface IShoppingService
    {
        decimal CalculateTotalPrice(Guid basketId, Guid clientId);
        ShoppingBasket AddProductToBasket(Guid clientId, Guid? basketId, Product product, int quantity);
        void RemoveProductFromBasket(Guid clientId, Guid basketId, Guid productId);
        ShoppingBasket GetBasketById(Guid basketId);

    }
}