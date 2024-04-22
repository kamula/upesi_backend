using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.accounts
{
    public class AccountDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal CurrentBalance { get; set; } = 0.00M;

        public Guid UserId { get; set; }
    }
}