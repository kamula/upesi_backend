using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        // additional user account details
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; } = string.Empty; //Prevent null entry in db
        public string? LastName { get; set; } = string.Empty; //Prevent null entry in db
        public string? PhoneNumber { get; set; } = string.Empty; //Prevent null entry in db

        // Link to Account's model
        public List<Account> Accounts { get; set; } = new List<Account>();

        public DateTime CreatedAt { get; private set; } // Not editable after creation
        public DateTime UpdatedAt { get; set; } // Editable and should be set on updates
    }
}