using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IWithdrawToAtmRepository
    {
        Task<string> PerformAtmWithdrawal(Guid userId, int bankAccountNumber, decimal amount);
    }
}