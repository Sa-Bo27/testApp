using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TechnicalExercice.Application;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using Xunit;

namespace TechnicalExercice.Tests.Application.Tests
{
    public class ApplicationTests
    {

        [Fact]
        public void Execute_GetAllClients_ShouldReturnClients()
        {
            // Arrange  
            var exceptedClients = new List<Client>
            {
                new Individual("John", "Doe"),
                new Professional("ACM Corp", 1_000_000)
            };

            var clientServiceMock = new Mock<IclientService>();
            clientServiceMock
            .Setup(x => x.GetAllClients())
            .Returns(exceptedClients);

            var getClients = new GetClients(clientServiceMock.Object);

            //Act
            var result = getClients.Execute();

            //Assert
            Assert.Equal(exceptedClients.Count, result.Count());
            Assert.Equal(exceptedClients, result);
                
        }

        [Fact]
        public void Execute_CreateClient_ShouldAddClient()
        {
            // Arrange  
            var newClient = new Individual("Jane", "Smith");

            var clientServiceMock = new Mock<IclientService>();
            // clientServiceMock
            // .Setup(x => x.AddClient(newClient));
            clientServiceMock.Setup(x => x.AddClient(newClient)).Verifiable();

            var createClient = new CreateClient(clientServiceMock.Object);

            //Act
            createClient.Execute(newClient.FirstName, newClient.LastName, ClientType.Individual, null, null);

            //Assert
            clientServiceMock.Verify(x => x.AddClient(It.Is<Individual>(i => i.FirstName == newClient.FirstName && i.LastName == newClient.LastName)), Times.Once());
        }

        [Fact]
        public async Task Execute_CalculateTotalBasket_ShouldReturnTotalPrice()
        {
            
            var client = new Individual("Alice", "Johnson");
            var basket = new ShoppingBasket("Test Basket", DateOnly.FromDateTime(DateTime.Now), client.Id);
            var product = new Product("Laptop", TypeProduct.Laptop);

            var clientServiceMock = new Mock<IclientService>();
            var productServiceMock = new Mock<IProductService>();
            var shoppingServiceMock = new Mock<IShoppingService>();

            clientServiceMock.Setup(x => x.AddClient(client));
            clientServiceMock.Setup(x => x.AddBasketToClient(client.Id, basket));
            shoppingServiceMock.Setup(x => x.AddProductToBasket(client.Id, basket.Id, product, 3));
            
            shoppingServiceMock
            .Setup(x => x.CalculateTotalPrice(basket.Id, client.Id))
            .Returns(3600);

           

            var calculateTotalBasket = new CalculateTotalBasket(clientServiceMock.Object, productServiceMock.Object, shoppingServiceMock.Object);

            //Act
            var result = await calculateTotalBasket.Execute(client.Id, basket.Id);

            //Assert
            Assert.Equal(3600, result);
        }
    }
}