using DoyleDispatchWebApp.Data.Enum;
using DoyleDispatchWebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.ViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public DateTime DropDate { get; set; }
        public IFormFile Image { get; set; }
        public string URL { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        public PackageCategory PackageCategory { get; set; }
        public string ClientId { get; set; }
    }
}
