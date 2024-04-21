using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces; 
using api.Mappers;
using api.Repository;
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
        public async Task<IActionResult> GetAll()
        {
            var funds = await _fundsTransferRepo.GetAllAsync();

            var fundsDto = funds.Select(s => s.ToFundsTransferDto());

            return Ok(fundsDto);
        }

    }
}