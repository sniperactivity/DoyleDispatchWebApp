using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Models
{
    public class Destination
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { get; set; }
        public string State { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{4}\-[0-9]{3}\-[0-9]{4}", ErrorMessage = "Please enter a valid Phone Number in the format, 9999-999-9999")]
        public string Contact { get; set; }
    }
}
