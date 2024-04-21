using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.fundsTransfer
{
    public class FundsTransferDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SourceAccountId { get; set; }

        public string? DestinationAccountNumber { get; set; }

        public Guid DestinationAccountId { get; set; }

        public decimal AmountTransferred { get; set; }
        public DateTime TransferDate { get; set; }
    }
}