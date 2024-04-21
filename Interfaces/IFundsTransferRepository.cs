using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IFundsTransferRepository
    {
        public Task<List<FundsTransfer>> GetAllAsync();

        Task<string> TransferFundsAsync(Guid sourceAccountId, Guid destinationAccountId, decimal amount);
    }
}