using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Application
{
    public class AddProductToBasket
    {
        private readonly IclientService _clientService;
        private readonly IShoppingService _shoppingService;
        private readonly IProductService _productService;

        public AddProductToBasket(IclientService clientService, IShoppingService shoppingService, IProductService productService)
        {
            _clientService = clientService;
            _shoppingService = shoppingService;
            _productService = productService;
        }

        public ShoppingBasket Execute(Guid clientId, Guid? basketId, string productName, int typeProduct, int quantity)
        {
            TypeProduct typeProd; 

            if((int)TypeProduct.Laptop == typeProduct)
            {
                typeProd = TypeProduct.Laptop;
            }
            else if((int)TypeProduct.HighEndPhone == typeProduct)
            {
                typeProd = TypeProduct.HighEndPhone;
            }
            else
            {
                typeProd = TypeProduct.MidRangePhone;
            }
            var product = _productService.GetProductByName(productName, typeProd);
            var basket = _shoppingService.AddProductToBasket(clientId, basketId, product, quantity);

            if(basket == null)
            {
                throw new Exception("Error adding product to basket");
            }

            return basket;
        }
    }
}