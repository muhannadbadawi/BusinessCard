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
    
    public partial class Client
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string hashPassword { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> lastLogin { get; set; }
    }
}
