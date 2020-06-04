using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SignUpModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
    public class UserInfo
    {
        [Required]
        [Display(Name = "User Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Enter Validation Code")]
        public string PassCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Promotions { get; set; }
        public string PhoneNumber { get; set; }
        public bool ShowPassCode { get; set; }
        public bool LoggedIn { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } set { } }
    }
}