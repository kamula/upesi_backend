using System;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AtmWithdrawRepository : IWithdrawToAtmRepository
    {
        private readonly ApplicationDBContext _context;

        public AtmWithdrawRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> PerformAtmWithdrawal(Guid userId, int bankAccountNumber, decimal amount)
        {
            // Handle concurrency 
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.UserId == userId);
                // var account = await _context.Accounts
                //     .FirstOrDefaultAsync(a => a.UserId == userId && a.AccountNumber == bankAccountNumber.ToString());

                if (account == null)
                {
                    return "Account does not exist.";
                }

                if (account.CurrentBalance < amount)
                {
                    return "Cannot withdraw amount greater than current balance.";
                }

                // Update account balance
                account.CurrentBalance -= amount;
                _context.Accounts.Update(account);

                // Create and add withdrawal record
                var atmWithdraw = new AtmWithdraw
                {
                    SourceAccountId = account.Id,
                    BankAccountNumber = bankAccountNumber,
                    AmountTransferred = amount,

                };

                await _context.AtmWithdraws.AddAsync(atmWithdraw);

                // Save all changes in the transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Withdrawal successful.";
            }
            catch (Exception ex)
            {
                // If an error occurs, roll back the transaction
                await transaction.RollbackAsync();
                return "Error during withdrawal: " + ex.Message;
            }
        }
    }
}
