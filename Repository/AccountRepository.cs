using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.accounts;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        public AccountRepository(ApplicationDBContext context)
        {
            _context = context;
        }



        public Task<List<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAccountWithDetailsByUserId(Guid userId)
        {
            var account = await _context.Accounts
                .Where(a => a.UserId == userId)
                .Include(a => a.User)
                .Include(a => a.AtmWithdraws)
                .Include(a => a.FundsTransfers)
                .FirstOrDefaultAsync();
            if (account == null)
            {
                return null;
            }
            return account;
        }


        public async Task<bool> UserHasAccountAsync(Guid userId)
        {
            return await _context.Accounts.AnyAsync(a => a.UserId == userId);
        }

        public async Task<Account> AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return account;

        }

        public async Task<bool> AccountNumberExists(string accountNumber)
        {
            return await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<string> UpdateAccountBalanceAsync(Guid userId, decimal amount, string sourceOfFunds)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            if (account == null)
                return "Account not found.";

            if (!Enum.TryParse(sourceOfFunds, out SourceOfFunds parsedSourceOfFunds))
                return "Invalid source of funds.";

            account.CurrentBalance += amount; // Adjust sign based on operation (deposit/withdrawal)
            account.SourceOfFunds = parsedSourceOfFunds;
            account.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return "Balance updated successfully.";
        }
    }
}