using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Repositories
{
    public class PackageRepo : IPackage
    {
        private readonly DataContext _context;

        public PackageRepo(DataContext context)
        {
            _context = context;
        }
        public bool Add(Package package)
        {
            _context.Add(package);
            return Save();
        }

        public bool Delete(Package package)
        {
            _context.Remove(package);
            return Save();
        }

        public async Task<IEnumerable<Package>> GetAll()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await _context.Packages.Include(i => i.Destination).FirstOrDefaultAsync(i => i.Id == id);     
        }
        public async Task<Package> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Packages.Include(i => i.Destination).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Package package)
        {
            _context.Update(package);
            return Save();
            //var employee = _context.Packages.Attach(package);
            //employee.State = EntityState.Modified;
            //_context.SaveChanges();
            //return package;
        }
    }
}
