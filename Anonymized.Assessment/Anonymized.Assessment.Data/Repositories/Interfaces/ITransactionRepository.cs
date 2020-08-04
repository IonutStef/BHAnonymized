using Anonymized.Assessment.Data.Contracts;
using Anonymized.Assessment.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<TransactionDto>
    {
        Task<ICollection<TransactionDto>> GetAllTransactionsForAccountIdAsync(string accountId);
    }
}