using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AtmWithdraw
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SourceAccountId { get; set; }  // Foreign Key for the Source account
        public Account? SourceAccount { get; set; }  // Navigation property for the Source account

        public string? BankName { get; set; } = string.Empty;
        public int? BankAccountNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        // Default value will be zero
        public decimal AmountTransferred { get; set; }
        public DateTime CreatedAt { get; private set; }


    }
}