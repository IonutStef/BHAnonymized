using Anonymized.Assessment.Data.Contracts;

namespace Anonymized.Assessment.Data.Dtos
{
    public class AccountDto : IDbEntity
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }
    }
}