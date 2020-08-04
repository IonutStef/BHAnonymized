using Anonymized.Assessment.Data.Dtos;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Data.Repositories
{
    public class TransactionRepository : EntityFrameworkRepository<TransactionDto>, ITransactionRepository
    {
        public TransactionRepository(AccountManagementContext context) : base(context)
        {
        }

        public async Task<ICollection<TransactionDto>> GetAllTransactionsForAccountIdAsync(string accountId) =>
            await DbSet.Where(t => t.AccountId == accountId).ToListAsync();
    }
}