using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using TechnicalExercice.Application;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using Xunit;

namespace TechnicalExercice.Tests.Api.Tests
{
    public class ClientEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ClientEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetTotalToPay_ReturnsExpectedValue()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var basketId = Guid.NewGuid();
            var expected = 123.45m;

            var clientServiceMock = new Mock<IclientService>();
            var shoppingServiceMock = new Mock<IShoppingService>();
            var productServiceMock = new Mock<IProductService>();

            // Create a simple concrete Client for the test
            //var testClient = new TestClient("Test client", clientId);
            var client = new Individual("Test FirstName", "Test LastName");
            clientServiceMock.Setup(s => s.GetClientById(clientId)).Returns(client);

            shoppingServiceMock.Setup(s => s.GetBasketById(basketId))
                .Returns(new ShoppingBasket("b", DateOnly.FromDateTime(DateTime.Now), clientId));

            shoppingServiceMock.Setup(s => s.CalculateTotalPrice(basketId, clientId)).Returns(expected);

            var calculate = new CalculateTotalBasket(clientServiceMock.Object, productServiceMock.Object, shoppingServiceMock.Object);

            var factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
                builder.ConfigureLogging(logging => { 
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.ClearProviders();
                });
                builder.ConfigureServices(services =>
                {
                    // Replace CalculateTotalBasket with our preconfigured instance
                    services.AddSingleton<CalculateTotalBasket>(calculate);
                });
            });

            var clientFactory = factory.CreateClient();

            // Act
            
            var response = await clientFactory.GetAsync($"/client/{clientId}/{basketId}/total-to-pay/");

            // Assert
            
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.True(false, $"Request failed with status code {response.StatusCode} and content: {content}");
            }
            else
            {
                Assert.Contains(expected.ToString(), content);
            }
            
        }

        [Fact]

        public async Task CalculateTotalBasket_ReturnsExpectedValue_InvalidClientId()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var basketId = Guid.NewGuid();

            var clientServiceMock = new Mock<IclientService>();
            var shoppingServiceMock = new Mock<IShoppingService>();
            var productServiceMock = new Mock<IProductService>();

            clientServiceMock.Setup(s => s.GetClientById(clientId)).Returns((Client)null);

            shoppingServiceMock.Setup(s => s.CalculateTotalPrice(basketId, clientId)).Throws(new Exception("Client not found"));

            var calculate = new CalculateTotalBasket(clientServiceMock.Object, productServiceMock.Object, shoppingServiceMock.Object);
        

            var factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
                builder.ConfigureLogging(logging => {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.ClearProviders();
                });
                builder.ConfigureServices(services =>
                {
                    // Replace CalculateTotalBasket with our preconfigured instance
                    services.AddSingleton<CalculateTotalBasket>(calculate);
                });
            });

            var clientFactory = factory.CreateClient();

            // Act
            var response = await clientFactory.GetAsync($"/client/{clientId}/{basketId}/total-to-pay/");

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Client not found", content);
        }

       
    }
}
