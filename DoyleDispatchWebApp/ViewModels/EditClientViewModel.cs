using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.ViewModels
{
    public class EditClientViewModel
    {
        public string Id { get; set; }
        [MaxLength(20, ErrorMessage = "Surname cannot exceed 20 characters")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public string? Surname { get; set; }
        [MaxLength(50, ErrorMessage = "Othername cannot exceed 50 characters")]
        public string? Othernames { get; set; }
        public string? Gender { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime Birthdate { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string? PhoneNumber { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
