﻿using Microsoft.EntityFrameworkCore;

namespace Mini_Inventory_System.Models.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
