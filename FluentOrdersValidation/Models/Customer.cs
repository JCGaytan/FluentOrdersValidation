﻿namespace FluentOrdersValidation.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Order Order { get; set; }
    }
}