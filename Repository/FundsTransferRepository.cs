using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FundsTransferRepository : IFundsTransferRepository
    {
        private readonly ApplicationDBContext _context;
        public FundsTransferRepository(ApplicationDBContext context)
        {
            _context = context;

        }
        public async Task<List<FundsTransfer>> GetAllAsync()
        {
            return await _context.FundsTransfers.ToListAsync();
        }

        public async Task<string> TransferFundsAsync(Guid sourceAccountId, Guid destinationAccountId, decimal amount)
        {
            // Handle concurrent database Update
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get source acc
                var sourceAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.UserId == sourceAccountId);
                if (sourceAccount == null)
                    return "Source account does not exist.";

                // Get destination acc
                var destinationAccount = await _context.Accounts.FindAsync(destinationAccountId);
                if (destinationAccount == null)
                    return "Destination account does not exist.";

                if (sourceAccount.CurrentBalance < amount)
                    return "Insufficient funds.";

                // Deduct from source
                sourceAccount.CurrentBalance -= amount;
                // Add to destination
                destinationAccount.CurrentBalance += amount;

                // Save changes and commit transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Transfer successful.";


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return $"An error occurred: {ex.Message}";
            }
        }
    }
}