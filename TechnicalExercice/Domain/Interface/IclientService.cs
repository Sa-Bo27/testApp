using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Domain.Interface
{
    public interface IclientService
    {
        Client GetClientById(Guid id);
        IEnumerable<Client> GetAllClients();
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Guid id);
        void AddBasketToClient(Guid clientId, ShoppingBasket basket);
        void RemoveBasketClient(Guid clientId, Guid basketId);
        void UpdateBasketClient(Guid clientId, Guid basketId, ShoppingBasket basket);
        
    }
}