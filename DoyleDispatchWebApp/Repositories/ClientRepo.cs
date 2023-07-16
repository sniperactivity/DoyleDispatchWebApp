using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public class ClientRepo : IClient
    {
        private readonly DataContext _context;

        public ClientRepo(DataContext context)
        {
            _context = context;
        }
        public bool Add(Client client)
        {
            _context.Add(client);
            return Save();
        }

        public bool Delete(Client client)
        {
            _context.Remove(client);
            return Save();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Client> GetClientById(string id)
        {
            return await _context.Users.FindAsync(id);         
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Client client)
        {
            _context.Update(client);
            return Save();
        }
    }
}
