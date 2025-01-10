using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessCard.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string password { get; set; }
    }
}