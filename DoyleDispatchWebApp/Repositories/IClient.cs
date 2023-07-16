using DoyleDispatchWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public interface IClient
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(string id);
        bool Add(Client client);
        bool Update(Client client);
        bool Delete(Client client);
        bool Save();
    }
}
