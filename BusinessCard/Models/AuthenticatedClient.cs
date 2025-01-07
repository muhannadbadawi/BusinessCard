using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCard.Models
{
    public class AuthenticatedClient
    {
        public Client client { get; set; }
        public List<Card> cards { get; set; } = new List<Card>();
    }
}