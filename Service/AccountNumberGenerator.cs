using api.Interfaces;
using System;
using System.Threading.Tasks;

public class AccountNumberGenerator
{
    private readonly IAccountRepository _accountRepo;

    public AccountNumberGenerator(IAccountRepository accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<string> GenerateUniqueAccountNumberAsync()
    {
        var random = new Random();
        string accountNumber;
        do
        {
            accountNumber = random.Next(1000000000, 2000000000).ToString();
        } while (await _accountRepo.AccountNumberExists(accountNumber));

        return accountNumber;
    }
}
