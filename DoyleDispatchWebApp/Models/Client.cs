using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Models
{
    public class Client : IdentityUser
    {
        public string Surname { get; set; }       
        public string Othernames { get; set; }       
        public string Gender { get; set; }     
        public DateTime Birthdate { get; set; }     
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        [ForeignKey("Destination")]
        public int? DestinationId { get; set; }
        public Destination Destination { get; set; }
        public ICollection<Package> Packages { get; set; }
    }
}
