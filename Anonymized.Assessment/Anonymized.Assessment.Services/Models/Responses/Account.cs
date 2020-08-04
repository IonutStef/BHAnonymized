using System.Collections.Generic;

namespace Anonymized.Assessment.Services.Models.Responses
{
    public class Account
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}