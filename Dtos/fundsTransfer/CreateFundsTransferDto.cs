using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.fundsTransfer
{
    public class CreateFundsTransferDto
    {

        [Required]
        public Guid DestinationAccountId { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public decimal AmountTransferred { get; set; }
    }
}