using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAccountRepository
    {
        public Task<List<Account>> GetAllAsync();
        public Task<Account?> GetByIdAsync(Guid id);

        public Task<Account> GetAccountWithDetailsByUserId(Guid userId);

        public Task<bool> UserHasAccountAsync(Guid userId);

        public Task<Account> AddAccountAsync(Account account);

        Task<bool> AccountNumberExists(string accountNumber);
        
    }
}