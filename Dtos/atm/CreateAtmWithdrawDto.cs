using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.atm
{
    public class CreateAtmWithdrawDto
    {
        public Guid SourceAccountId { get; set; }

        public int? BankAccountNumber { get; set; }

        public decimal AmountTransferred { get; set; }
    }
}