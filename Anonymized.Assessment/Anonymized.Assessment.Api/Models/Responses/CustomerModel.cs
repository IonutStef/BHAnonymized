using System.Collections.Generic;

namespace Anonymized.Assessment.Api.Models.Responses
{
    /// <summary>
    /// A customer.
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Unique identifier of the customer.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First name of the customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Surname of the customer.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Balance of the customer.
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// List of accounts associated to this customer.
        /// </summary>
        public ICollection<AccountModel> Accounts { get; set; }
    }
}