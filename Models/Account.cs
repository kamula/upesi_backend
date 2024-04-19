using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "decimal(18,2)")]

        // Default value will be zero
        public decimal CurrentBalance { get; set; } = 0.00M;

        // randomly generated 10 digit number
        public string AccountNumber { get; set; } = string.Empty;

        // Savings, current,money market
        public string AccountType { get; set; } = string.Empty;

        // Foreign Key for User
        public Guid UserId { get; set; }
        public User? User { get; set; } // Navigation property

        public DateTime CreatedAt { get; private set; } // Not editable after creation
        public DateTime UpdatedAt { get; set; } // Editable and should be set on updates

        // Funds Transfer foreign key config
        public List<FundsTransfer> FundsTransfers { get; set; } = new List<FundsTransfer>();

        // ATM withdraw foreign key config
        public List<AtmWithdraw> AtmWithdraws { get; set; } = new List<AtmWithdraw>();
    }
}