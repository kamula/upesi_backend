using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserFinancialRepository : IUserFinancialRepository
    {
        private readonly ApplicationDBContext _context;

        public UserFinancialRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<UserFinancialDetailsDto> GetUserFinancialDetailsAsync(Guid userId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            bool hasCreatedAccount = account != null;  // Check if the account exists

            if (account == null)
            {
                return new UserFinancialDetailsDto
                {
                    CurrentBalance = 0,
                    TotalAmountTransacted = 0,
                    TotalAmountWithdrawn = 0,
                    RecentTransfers = new List<TransactionDto>(),
                    RecentWithdraws = new List<WithdrawDto>(),
                    HasCreatedAccount = hasCreatedAccount
                };
            }

            // Get total funds transacted
            var totalTransacted = await _context.FundsTransfers
            .Where(ft => ft.SourceAccountId == account.Id)
            .SumAsync(ft => ft.AmountTransferred);

            // Get total funds withdrawn
            var totalWithdrawn = await _context.AtmWithdraws
            .Where(aw => aw.SourceAccountId == account.Id)
            .SumAsync(aw => aw.AmountTransferred);

            // Recent five transactions
            var recentTransfers = await _context.FundsTransfers
            .Where(ft => ft.SourceAccountId == account.Id)
            .OrderByDescending(ft => ft.CreatedAt)
            .Take(5)
            .Select(ft => new TransactionDto
            {
                Id = ft.Id,
                Amount = ft.AmountTransferred,
                DestinationAccount = ft.DestinationAccount.AccountNumber,
                Date = ft.CreatedAt
            })
            .ToListAsync();

            // Recent funds withdraw
            var recentWithdraws = await _context.AtmWithdraws
            .Where(aw => aw.SourceAccountId == account.Id)
            .OrderByDescending(aw => aw.CreatedAt)
            .Take(5)
            .Select(aw => new WithdrawDto
            {
                Id = aw.Id,
                Amount = aw.AmountTransferred,
                BankAccountNumber = aw.BankAccountNumber ?? 0,
                Date = aw.CreatedAt
            })
            .ToListAsync();

            return new UserFinancialDetailsDto
            {
                CurrentBalance = account.CurrentBalance,
                TotalAmountTransacted = totalTransacted,
                TotalAmountWithdrawn = totalWithdrawn,
                RecentTransfers = recentTransfers,
                RecentWithdraws = recentWithdraws,
                HasCreatedAccount = hasCreatedAccount
            };
        }
    }
}