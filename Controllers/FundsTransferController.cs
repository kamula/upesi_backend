using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.fundsTransfer;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/funds/transfer")]
    [ApiController]
    public class FundsTransferController : ControllerBase
    {
        private readonly IFundsTransferRepository _fundsTransferRepo;
        public FundsTransferController(IFundsTransferRepository fundsTransferRepo)
        {
            _fundsTransferRepo = fundsTransferRepo;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var funds = await _fundsTransferRepo.GetAllAsync();

            var fundsDto = funds.Select(s => s.ToFundsTransferDto());

            return Ok(fundsDto);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TransferFunds([FromBody] CreateFundsTransferDto transferDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (transferDto == null || transferDto.AmountTransferred <= 0)
                return BadRequest("Invalid transfer details.");
            
            // Get user Id from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return BadRequest("User ID not found.");

            Guid sourceAccountId = new Guid(userId);

            var result = await _fundsTransferRepo.TransferFundsAsync(
                sourceAccountId,
                transferDto.DestinationAccountId,
                transferDto.AmountTransferred);

            if (result == "Transfer successful.")
                return Ok(result);

            return BadRequest(result);


        }

    }
}