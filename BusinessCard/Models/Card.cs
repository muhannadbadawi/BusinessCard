//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessCard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        public int Id { get; set; }
        public int clientId { get; set; }
        public string companyName { get; set; }
        public string location { get; set; }
        public string jobTitle { get; set; }
        public string logoPath { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}