namespace Anonymized.Assessment.Api.Models.Responses
{
    /// <summary>
    /// Transaction associated to an account.
    /// </summary>
    public class TransactionModel
    {
        /// <summary>
        /// Unique identifier of the transaction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Amount of the transaction.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Unique identifier of the account, associated to this transaction.
        /// </summary>
        public string AccountId { get; set; }
    }
}