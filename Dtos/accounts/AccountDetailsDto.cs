using api.Dtos.atm;
using api.Dtos.auth;
using api.Dtos.fundsTransfer;
using api.Models;

public class AccountDetailsDto
{
    public Guid AccountId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public NewUserDto User { get; set; }
    public List<AtmWithdrawDto> AtmWithdraws { get; set; }
    public List<FundsTransferDto> FundsTransfers { get; set; }

    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
