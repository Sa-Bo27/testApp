using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using TechnicalExercice.Infrastructure.Context;

namespace TechnicalExercice.Infrastructure.Services
{
    public class ClientService : IclientService
    {
        private readonly MyDbContext _context;
        public ClientService(MyDbContext context)
        {
            _context = context;
        }
        public void AddBasketToClient(Guid clientId, ShoppingBasket basket)
        {
            var client = _context.Clients.Find(clientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            client.CreateBasket(basket);
            _context.SaveChanges();
        }

        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void DeleteClient(Guid id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClientById(Guid id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            return client;
        }

        public void RemoveBasketClient(Guid clientId, Guid basketId)
        {
            var client = _context.Clients.Find(clientId);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            client.DeleteBasket(basketId);
            _context.SaveChanges();
        }

        public void UpdateBasketClient(Guid clientId, Guid basketId, ShoppingBasket basket)
        {
            var client = _context.Clients.Find(clientId) ?? throw new Exception("Client not found");
            client.UpdateBasket(basket);
            _context.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }
    }
}