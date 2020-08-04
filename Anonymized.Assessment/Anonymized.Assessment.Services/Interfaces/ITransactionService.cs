using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Services.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Create a transaction for the specified <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The requeest containing the data 
        /// after which the transaction should be created.</param>
        /// <returns>The created <see cref="Transaction"/>.</param>
        Task<Transaction> InitiateTransactionAsync(CreateTransactionRequest request);
    }
}