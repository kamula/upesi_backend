using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.atm;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/atm")]
    [ApiController]
    public class AtmWithdrawController : ControllerBase
    {
        private readonly IWithdrawToAtmRepository _atmWithdrawRepo;
        public AtmWithdrawController(IWithdrawToAtmRepository atmWithdrawRepo)
        {
            _atmWithdrawRepo = atmWithdrawRepo;
        }
        [HttpPost("withdraw")]
        [Authorize]
        public async Task<IActionResult> Withdraw([FromBody] CreateAtmWithdrawDto withdrawDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            var response = await _atmWithdrawRepo.PerformAtmWithdrawal(Guid.Parse(userId), withdrawDto.BankAccountNumber, withdrawDto.AmountTransferred);
            if (response == "Withdrawal successful.")
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}