using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.atm
{
    public class AtmWithdrawDto
    {
        public Guid Id { get; set; }

        public Guid SourceAccountId { get; set; }

        public int? BankAccountNumber { get; set; }

        public decimal AmountTransferred { get; set; }

        public DateTime TransferDate { get; set; }
    }
}