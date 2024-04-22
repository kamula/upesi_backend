using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    // Moved outside of the Account class
    public enum SourceOfFunds
    {
        Deposit,
        TransferredFunds
    }

    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; } = 0.00M;
        
        public string AccountNumber { get; set; } = string.Empty;

        public User User { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [DefaultValue("Deposit")] // Ensure this default value is set in the database manually if needed
        public SourceOfFunds SourceOfFunds { get; set; } = SourceOfFunds.Deposit;

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public List<FundsTransfer> FundsTransfers { get; set; } = new List<FundsTransfer>();
        public List<AtmWithdraw> AtmWithdraws { get; set; } = new List<AtmWithdraw>();
    }
}
