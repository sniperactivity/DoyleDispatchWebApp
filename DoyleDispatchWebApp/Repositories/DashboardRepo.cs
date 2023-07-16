using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public class DashboardRepo : IDashboard
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<List<Package>> GetAllUserPackages()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPackages = _context.Packages.Where(r => r.Client.Id == curUser);
            return userPackages.ToList();
        }
        public async Task<IEnumerable<Package>> GetAllPackages()
        {
            return await _context.Packages.ToListAsync();
        }
        public async Task<Client> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Client> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();   
        }

        public bool Update(Client user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
