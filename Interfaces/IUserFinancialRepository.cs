using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.Interfaces
{
    public interface IUserFinancialRepository
    {
        Task<UserFinancialDetailsDto> GetUserFinancialDetailsAsync(Guid userId);
    }
}