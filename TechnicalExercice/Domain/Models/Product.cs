using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercice.Domain.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public TypeProduct TypeProduct { get; private set; }
        public Dictionary<ClientType, decimal> Price { get; private set; }
        public Currency Currency { get; private set; } = Currency.EUR;



        public Guid GetId() => Id;
        public string GetName() => Name;
        public decimal GetPrice(ClientType type) => Price[type];

        public Product(string name, TypeProduct typeProduct)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = new Dictionary<ClientType, decimal>();
            TypeProduct = typeProduct;
            SetPrice(typeProduct);
        }

        private void SetPrice(TypeProduct type)
        {
            if (type == TypeProduct.HighEndPhone)
            {
                Price.Add(ClientType.Individual, 1500);
                Price.Add(ClientType.Professional, 1150);
                Price.Add(ClientType.ProfessionalHighThan10M, 1000);
            }
            else if (type == TypeProduct.MidRangePhone)
            {
                Price.Add(ClientType.Individual, 800);
                Price.Add(ClientType.Professional, 600);
                Price.Add(ClientType.ProfessionalHighThan10M, 550);
            }
            else if (type == TypeProduct.Laptop)
            {
                Price.Add(ClientType.Individual, 1200);
                Price.Add(ClientType.Professional, 1000);
                Price.Add(ClientType.ProfessionalHighThan10M, 900);
            }
        }
    }
}