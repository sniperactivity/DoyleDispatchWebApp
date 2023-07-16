using DoyleDispatchWebApp.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PackageName { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime DropDate { get; set; }
        public string Image { get; set; }
        [ForeignKey("Destination")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
        public Nullable<byte> Status { get; set; }
        public PackageCategory PackageCategory { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }
    }
}
