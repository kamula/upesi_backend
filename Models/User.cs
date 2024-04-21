using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace api.Models
{
    public class User : IdentityUser<Guid>
    {
        // Additional user account details        
        public string? FirstName { get; set; } = string.Empty; // Prevent null entry in db
        public string? LastName { get; set; } = string.Empty; // Prevent null entry in db
        public Guid? AccountId { get; set; }  // Foreign key for the Account
        public Account Account { get; set; }  // Navigation property
        public DateTime CreatedAt { get; private set; } = DateTime.Now; // Set default value to now
        public DateTime UpdatedAt { get; set; } // Editable and should be set on updates

        
    }
}
