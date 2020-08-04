using Anonymized.Assessment.Data.Contracts;

namespace Anonymized.Assessment.Data.Dtos
{
    public class TransactionDto : IDbEntity
    {
        public string Id { get; set; }

        public double Amount { get; set; }

        public string AccountId { get; set; }
    }
}