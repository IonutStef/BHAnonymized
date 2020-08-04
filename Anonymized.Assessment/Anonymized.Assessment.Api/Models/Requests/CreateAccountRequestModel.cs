using System.ComponentModel.DataAnnotations;

namespace Anonymized.Assessment.Api.Models.Requests
{
    /// <summary>
    /// Request for opening an account.
    /// </summary>
    public class CreateAccountRequestModel
    {
        /// <summary>
        /// Unique identifier of the customer, for which the account will be opened.
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Initial credit associated to the account.
        /// If greater than 0, a transaction, will be initiated.
        /// </summary>
        public double InitialCredit { get; set; }
    }
}