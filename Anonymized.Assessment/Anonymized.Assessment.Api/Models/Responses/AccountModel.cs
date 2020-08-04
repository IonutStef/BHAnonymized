using System.Collections.Generic;

namespace Anonymized.Assessment.Api.Models.Responses
{
    /// <summary>
    /// Account associated to a customer.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Unique identifier of the account.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Unique identifier of the customer, asociated to this account.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// List of transactions, associated to this account.
        /// </summary>
        public ICollection<TransactionModel> Transactions { get; set; }
    }
}