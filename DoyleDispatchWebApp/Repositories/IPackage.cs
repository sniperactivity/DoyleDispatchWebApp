using DoyleDispatchWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public interface IPackage
    {
        Task<IEnumerable<Package>> GetAll();
        Task<Package> GetByIdAsync(int id);
        Task<Package> GetByIdAsyncNoTracking(int id);
        bool Add(Package package);
        public bool Update(Package package);
        bool Delete(Package package);
        bool Save();
    }
}
