using Anonymized.Assessment.Services.Models.Requests;
using Anonymized.Assessment.Services.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get the <see cref="Customer"/> for the <paramref name="customerId"/>.
        /// </summary>
        /// <param name="customerId">Unique identifier of the customer.</param>
        /// <returns>The <see cref="Customer"/> with the specified <paramref name="customerId"/>.</returns>
        Task<Customer> GetCustomerAsync(string customerId);

        /// <summary>
        /// Insert the <paramref name="customers"/> into the database.
        /// </summary>
        /// <param name="customers">The list of Customers to be inserted.</param>
        /// <returns>The list <see cref="Customer"/> that were inserted in the database./></returns>
        ICollection<Customer> InsertCustomers(ICollection<CreateCustomerRequest> customers);
    }
}