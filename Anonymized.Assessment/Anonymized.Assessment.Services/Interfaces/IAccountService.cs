using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Create an account for the specified <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The request containing the data 
        /// after which the account should be created.</param>
        /// <returns>The created <see cref="Account"/>.</returns>
        Task<Account> OpenAccountAsync(CreateAccountRequest request);
    }
}