using System;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAccountService
    {
        Task<string> GenerateUniqueAccountNumberAsync();
        
        // Additional service methods can be defined here:

        // Create an account with the necessary initial setup
        Task<Account> CreateAccountAsync(Guid userId, decimal initialDeposit);

        // Validate if a user already has an account
        Task<bool> UserHasAccountAsync(Guid userId);

        // Retrieve account details for a specific user
        Task<Account> GetAccountDetailsByUserId(Guid userId);
    }
}
