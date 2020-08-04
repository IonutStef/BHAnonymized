namespace Anonymized.Assessment.Services.Models.Requests
{
    public class CreateTransactionRequest
    {
        public string AccountId { get; set; }

        public double Amount { get; set; }
    }
}