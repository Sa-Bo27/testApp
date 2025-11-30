using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Models;
using Xunit;

namespace TechnicalExercice.Tests.Domain.Tests
{
    public class DomainTests
    {
        [Fact]
        public void ClientTypeEnum_ShouldContainExpectedValues()
        {
            // Arrange
            var expectedValues = new[] { "Individual", "Professional", "ProfessionalHighThan10M" };

            // Act
            var actualValues = Enum.GetNames(typeof(ClientType));

            // Assert
            Assert.Equal(expectedValues, actualValues);
        }

        [Fact]
        public void TypeProductEnum_ShouldContainExpectedValues()
        {
            // Arrange
            var expectedValues = new[] { "HighEndPhone", "MidRangePhone", "Laptop" };

            // Act
            var actualValues = Enum.GetNames(typeof(TypeProduct));

            // Assert
            Assert.Equal(expectedValues, actualValues);
        }

        [Fact]
        public void Product_Price_ShouldBeSetCorrectly_BasedOnClientType()
        {
            // Arrange
            var product = new Product("Test Phone", TypeProduct.HighEndPhone);

            // Act & Assert
            Assert.Equal(1500, product.GetPrice(ClientType.Individual));
            Assert.Equal(1150, product.GetPrice(ClientType.Professional));
            Assert.Equal(1000, product.GetPrice(ClientType.ProfessionalHighThan10M));
        }

        [Fact]
        public void Client_ShouldBeCreated_By_ClientType()
        {
            // Arrange
            var individual = new Individual("John", "Doe");
            var professional = new Professional("Acme Corp", 10_000_000);


            // Act & Assert
            Assert.Equal("John", individual.FirstName);
            Assert.Equal("Doe", individual.LastName);
            Assert.Equal(ClientType.Individual, individual.GetType());
            Assert.Equal("Acme Corp", professional.CompanyName);
            Assert.Equal(10_000_000, professional.Revenue);
            Assert.Equal(ClientType.ProfessionalHighThan10M, professional.GetType());
        }
    }
}