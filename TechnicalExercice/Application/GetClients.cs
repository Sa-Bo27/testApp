using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Application
{
    public class GetClients
    {
        private readonly IclientService _clientService;

        public GetClients(IclientService clientService)
        {
            _clientService = clientService;
        }

        public IEnumerable<Client> Execute()
        {
            return _clientService.GetAllClients();
        }
    }
}