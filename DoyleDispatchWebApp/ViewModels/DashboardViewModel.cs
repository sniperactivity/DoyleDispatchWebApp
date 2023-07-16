using DoyleDispatchWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public List<Package> Packages { get; set; }
#pragma warning disable IDE1006 // Naming Styles
        public IEnumerable<Package> packages { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
