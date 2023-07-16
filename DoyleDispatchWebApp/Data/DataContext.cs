using DoyleDispatchWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Data
{
    public class DataContext : IdentityDbContext<Client>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
    }
}
