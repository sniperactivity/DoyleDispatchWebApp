using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Please Confirm Your Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password Does Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
