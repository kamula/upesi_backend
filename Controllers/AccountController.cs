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

        private readonly IUserFinancialRepository _financialRepository;


        public AccountController(IAccountRepository accountRepo, AccountNumberGenerator accountNumberGenerator, IUserFinancialRepository financialRepository)
        {
            _accountRepo = accountRepo;
            _accountNumberGenerator = accountNumberGenerator;
            _financialRepository = financialRepository;



        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountRepo.GetAllAccountDetailsAsync();
            return Ok(accounts);
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


        [HttpPatch("deposit")]
        [Authorize]
        public async Task<IActionResult> DepositBalance([FromBody] AccountDepositDto accountDepositDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Retrieve userId from the claims
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized("User ID is invalid.");
            }

            var amount = accountDepositDto.depositAmount;
            if (amount <= 0)
                return BadRequest("Invalid amount.");

            var result = await _accountRepo.UpdateAccountBalanceAsync(userId, amount, "Deposit");
            if (result == "Balance updated successfully.")
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet("details")]
        [Authorize]
        public async Task<IActionResult> GetFinancialDetails()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized("User ID is invalid.");
            }

            var details = await _financialRepository.GetUserFinancialDetailsAsync(userId);
            if (details == null)
                return NotFound("User or financial details not found.");

            return Ok(details);
        }










    }
}