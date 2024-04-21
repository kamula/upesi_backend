using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.accounts;
using api.Models;

namespace api.Mappers
{
    public static class AccountsMapper
    {
        public static AccountDto ToAccountTransferDto(this Account accountTransferModel)
        {
            return new AccountDto
            {
                Id = accountTransferModel.Id,
                CurrentBalance = accountTransferModel.CurrentBalance,
                UserId = accountTransferModel.UserId
            };
        }
    }
}