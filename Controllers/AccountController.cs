using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.accounts;
using api.Dtos.atm;
using api.Dtos.fundsTransfer;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly AccountNumberGenerator _accountNumberGenerator;


        public AccountController(IAccountRepository accountRepo, AccountNumberGenerator accountNumberGenerator)
        {
            _accountRepo = accountRepo;
            _accountNumberGenerator = accountNumberGenerator;



        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountRepo.GetAllAsync();

            var fundsDto = accounts.Select(s => s.ToAccountTransferDto());

            return Ok(fundsDto);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            // Create account
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }
            // Check if user has account
            Guid guidUserId = Guid.Parse(userId);
            bool hasAccount = await _accountRepo.UserHasAccountAsync(guidUserId);

            if (hasAccount)
            {
                return BadRequest("User already has an account.");
            }

            var newAccount = new Account
            {
                UserId = guidUserId,
                AccountNumber = await _accountNumberGenerator.GenerateUniqueAccountNumberAsync(),
                CurrentBalance = createAccountDto.InitialDeposit,

            };
            var addedAccount = await _accountRepo.AddAccountAsync(newAccount);

            return Ok("Account created successfully");
        }

        [HttpGet("details")]
        [Authorize]
        public async Task<IActionResult> GetUserAccountDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("user id not found");
            }
            var account = await _accountRepo.GetAccountWithDetailsByUserId((Guid.Parse(userId)));

            var accountDetailsDto = new AccountDetailsDto
            {
                // AtmWithdraws = account.AtmWithdraws.Select(w => new AtmWithdrawDto
                // {
                //     Id = w.Id,
                //     AmountTransferred = w.AmountTransferred,
                //     BankAccountNumber = w.BankAccountNumber,
                //     TransferDate = w.CreatedAt

                // }).ToList(),
                // FundsTransfers = account.FundsTransfers.Select(t => new FundsTransferDto
                // {
                //     Id = t.Id,
                //     DestinationAccountNumber = t.DestinationAccount.AccountNumber,
                //     DestinationAccountId = t.DestinationAccountId,
                //     TransferDate = t.CreatedAt,

                // }).ToList()
            };
            return Ok(accountDetailsDto);
        }


    }
}