using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Application
{
    public class CreateClient
    {
        private readonly IclientService _clientService;

        public CreateClient(IclientService clientService)
        {
            _clientService = clientService;
        }

        public void Execute(string firstName, string lastName, ClientType clientType, string? companyName, decimal? revenue)
        {
            // Implementation to create a client
            if(clientType == ClientType.Individual)
            {
                var client = new Individual(firstName, lastName);
                _clientService.AddClient(client);
            }
            else if(clientType == ClientType.Professional && companyName != null && revenue.HasValue)
            {
                var client = new Professional(companyName, revenue.Value);
                _clientService.AddClient(client);
            }
            
        }
    }
}