using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class FundsTransferRepository : IFundsTransferRepository
    {
        private readonly ApplicationDBContext _context;
        public FundsTransferRepository(ApplicationDBContext context)
        {
            _context = context;

        }
        public async Task<List<FundsTransfer>> GetAllAsync()
        {
            return await _context.FundsTransfers.ToListAsync();
        }
    }
}