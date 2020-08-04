using Anonymized.Assessment.Services.Models.Requests;
using System.Collections.Generic;

namespace Anonymized.Assessment.Api.Infrastructure
{
    /// <summary>
    /// Settings containing the seed data to be added in the database.
    /// </summary>
    public class CustomersDataSeed : ICustomersDataSeed
    {
        /// <summary>
        /// The seed data to be added in the database.
        /// </summary>
        public ICollection<CreateCustomerRequest> Data { get; set; }
    }
}