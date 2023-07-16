using DoyleDispatchWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public interface IDashboard
    {
        Task<List<Package>> GetAllUserPackages();
        Task<IEnumerable<Package>> GetAllPackages();
        Task<Client> GetUserById(string id);
        Task<Client> GetByIdNoTracking(string id);
        bool Update(Client user);
        bool Save();
    }
}
