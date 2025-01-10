using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCard.Models
{
    public class MyClient
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string email { get; set; }

            public string phone { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string password { get; set; }
            [Compare("password")]
            public string confirmPassword { get; set; }

            [Required(ErrorMessage = "Username is required.")]
            public string username { get; set; }

            [Required(ErrorMessage = "Name is required.")]
            public string name { get; set; }

            public Nullable<System.DateTime> lastLogin { get; set; }
    }
}