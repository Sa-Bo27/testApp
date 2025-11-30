using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using TechnicalExercice.Infrastructure.Context;

namespace TechnicalExercice.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _context;
        public ProductService(MyDbContext context)
        {
            _context = context;
            
        }
        public decimal GetPrice(Guid productId, ClientType clientType)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId) ?? throw new Exception("Product not found");
            return product.GetPrice(clientType);
        }

        public Product GetProductByName(string productName, TypeProduct typeProduct)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == productName);
            if (product == null)
            {
                string name;
                name = CreateProductName(typeProduct);

                product = new Product(name, typeProduct);
                _context.Products.Add(product);
            }
            return product;
        }

        private static string CreateProductName(TypeProduct typeProduct)
        {
            string name;
            if (typeProduct == TypeProduct.Laptop)
            {
                name = "Laptop";
            }
            else if (typeProduct == TypeProduct.HighEndPhone)
            {
                name = "High End Phone";
            }
            else if (typeProduct == TypeProduct.MidRangePhone)
            {
                name = "Mid Range Phone";
            }
            else
            {
                name = "Default Other Product";
            }

            return name;
        }
    }
}