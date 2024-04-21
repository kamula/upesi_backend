using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.fundsTransfer;
using api.Models;

namespace api.Mappers
{
    public static class FundsTransferMapper
    {
        public static FundsTransferDto ToFundsTransferDto(this FundsTransfer fundsTransferModel)
        {
            return new FundsTransferDto
            {
                Id = fundsTransferModel.Id,
                SourceAccountId = fundsTransferModel.SourceAccountId,
                DestinationAccountId = fundsTransferModel.DestinationAccountId,
            };
        }
    }
}