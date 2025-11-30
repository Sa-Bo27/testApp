using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;

namespace TechnicalExercice.Application
{
    public class CalculateTotalBasket
    {
        private readonly IclientService _clientService;
        private readonly IProductService _productService;
        private readonly IShoppingService _shoppingService;

        public CalculateTotalBasket(IclientService clientService, IProductService productService, IShoppingService shoppingService)
        {
            _clientService = clientService;
            _productService = productService;
            _shoppingService = shoppingService;
        }

        public async Task<decimal> Execute(Guid clientId, Guid basketId)
        {
            ArgumentException.ThrowIfNullOrEmpty(clientId.ToString(), nameof(clientId));
            ArgumentException.ThrowIfNullOrEmpty(basketId.ToString(), nameof(basketId));

            
            return _shoppingService.CalculateTotalPrice(basketId, clientId);
               
             
        }
    }
}