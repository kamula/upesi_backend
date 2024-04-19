using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class FundsTransfer
    {

        // Funds Transfer model
        public Guid AccountId { get; set; }
        [ForeignKey("SourceAccount")]
        public Guid SourceAccountId { get; set; }  // Foreign Key for the source account
        public Account? SourceAccount { get; set; }  // Navigation property for the source account

        [ForeignKey("DestinationAccount")]
        public Guid DestinationAccountId { get; set; }  // Foreign Key for the destination account
        public Account? DestinationAccount { get; set; }  // Navigation property for the destination account

        [Column(TypeName = "decimal(18,2)")]

        // Default value will be zero
        public decimal AmountTransferred { get; set; }
        public DateTime CreatedAt { get; private set; }

    }
}