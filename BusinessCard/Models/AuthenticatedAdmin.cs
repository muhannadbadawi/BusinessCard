using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCard.Models
{
    public class AuthenticatedAdmin
    {
        public Admin admin { get; set; }
        public string clientCount { get; set; }
        public string cardCount { get; set; }
    }
}