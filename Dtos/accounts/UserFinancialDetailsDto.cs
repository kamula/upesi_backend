using System;
using System.Collections.Generic;

namespace api.Dtos
{
    public class UserFinancialDetailsDto
    {
        public decimal CurrentBalance { get; set; }
        public decimal TotalAmountTransacted { get; set; }
        public decimal TotalAmountWithdrawn { get; set; }
        public List<TransactionDto> RecentTransfers { get; set; }
        public List<WithdrawDto> RecentWithdraws { get; set; }
    }

    public class TransactionDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public string DestinationAccount { get; set; }
        public DateTime Date { get; set; }
    }

    public class WithdrawDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public int BankAccountNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
