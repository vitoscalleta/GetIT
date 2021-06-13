using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static GetIT.Utility.Classes.CustomValidations;

namespace GetIT.Models
{
    public class RegisterUserVM
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        [EmailDomainValidation(domainName:"getit.com", errorMessage: "The email id should be of the domain: getit.com ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Passwords must match")]
        public string ConfirmPassword { get; set; }


        public string City { get; set; }
    }
}
